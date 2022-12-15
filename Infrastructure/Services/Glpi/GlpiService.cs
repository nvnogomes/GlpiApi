using GLPIService.Application.Common.Dtos;
using GLPIService.Application.Common.Enums;
using GLPIService.Application.Common.Exceptions;
using GLPIService.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System.Dynamic;
using System.Net;
using System.Text;

namespace GLPIService.Infrastructure.Services.Glpi {

    public class GlpiService : IGlpiService, IDisposable {

        private readonly GlpiConfiguration _glpiConfig;
        private readonly RestClient _restClient;


        public GlpiService(IOptions<GlpiConfiguration> settings) {
            _glpiConfig = settings.Value;

            var options = new RestClientOptions(_glpiConfig.ApiUrl) {
                ThrowOnAnyError = false,
                MaxTimeout = -1,
                Expect100Continue = false,
            };
            _restClient = new RestClient(options)
                .AddDefaultHeader("App-Token", _glpiConfig.AppToken);
        }


        private static string BuildExceptionMessage(RestResponse response) {
            return $"{response.StatusCode} - {response.ErrorMessage}";
        }



        private async Task<byte[]> MakeGlpiRequestRaw(RestRequest request) {
            try {
                request.AddHeader("App-Token", _glpiConfig.AppToken);
                request.AddHeader("Session-Token", await GetSessionToken());

                var response = await _restClient.ExecuteAsync(request);
                if (response.IsSuccessStatusCode && response.RawBytes is not null) {
                    return response.RawBytes;
                }
                else {
                    throw new GlpiException($"Invalid response ({response.Content})");
                }
                throw new GlpiException(BuildExceptionMessage(response));

            } catch (Exception ex) when (ex is not GlpiException) {
                var errorMessage = $"Error while trying {request.Resource}. ({ex.InnerException})";
                Log.Fatal(ex, errorMessage);

                throw new GlpiException(errorMessage, ex);
            }
        }

        private async Task<string> MakeGlpiRequest(RestRequest request) {
            try {
                request.AddHeader("App-Token", _glpiConfig.AppToken);
                request.AddHeader("Session-Token", await GetSessionToken());

                var response = await _restClient.ExecuteAsync(request);
                if (response.IsSuccessful) {
                    return response.Content ?? "";
                }
                else {
                    throw new GlpiException($"Invalid response ({response.Content})");
                }
                throw new GlpiException(BuildExceptionMessage(response));

            } catch (Exception ex) when (ex is not GlpiException) {
                var errorMessage = $"Error while trying {request.Resource}. ({ex.InnerException})";
                Log.Fatal(ex, errorMessage);

                throw new GlpiException(errorMessage, ex);
            }
        }


        private async Task<T> MakeGlpiRequest<T>(RestRequest request) {
            try {
                var response = await MakeGlpiRequest(request);

                var data = JsonConvert.DeserializeObject<T>(response ?? "");
                if (data is not null) {
                    return data;
                }

                throw new GlpiException($"Invalid response ({response})");
            } catch (Exception ex) when (ex is not GlpiException) {
                Log.Fatal(ex, $"Error while trying {request.Resource}.");

                throw new GlpiException($"Error while trying {request.Resource}.", ex);
            }
        }


        private async Task<string> GetSessionToken() {

            // add authorization tokens
            if (string.IsNullOrWhiteSpace(_glpiConfig.SessionToken)) {
                await Login();
            }

            return _glpiConfig.SessionToken;
        }


        ///
        /// PUBLIC METHODS
        ///
        public async Task<string> GenericRequest(string method, object? parameters) {

            var request = new RestRequest(method);

            if (parameters is not null) {
                var jsonParams = JsonConvert.SerializeObject(parameters);

                request.Method = Method.Post;
                request.AddJsonBody(jsonParams);
            }

            return await MakeGlpiRequest<string>(request);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Login() {

            try {
                var action = GLPIAction.InitSession;

                var request = new RestRequest(action.Value, Method.Get)
                    .AddHeader("Authorization", "Basic " + _glpiConfig.UserToken);

                var response = await _restClient.ExecuteGetAsync<SessionTokenDto>(request);
                if (!string.IsNullOrWhiteSpace(response.Data?.Token)) {
                    _glpiConfig.SessionToken = response.Data.Token;
                }
                else {
                    throw new Exception("Session token not received");
                }
            } catch (Exception ex) {
                Log.Fatal(ex, "Error while trying to login in GLPI");

                throw new GlpiException("Error while trying to login in GLPI", ex);
            }
        }





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Logout() {
            var action = GLPIAction.KillSession;

            var request = new RestRequest(action.Value, Method.Get);

            var response = await _restClient.ExecuteGetAsync<SessionTokenDto>(request);
            if (!string.IsNullOrWhiteSpace(response.Data?.Token)) {
                _glpiConfig.SessionToken = response.Data.Token;
            }

            throw new Exception("Logout uncessful");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public async Task<TicketDto> GetTicket(int ticketId) {
            var action = GLPIAction.Ticket;
            var url = $"{action.Value}/{ticketId}";

            var request = new RestRequest(url, Method.Get);

            return await MakeGlpiRequest<TicketDto>(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tObject"></param>
        /// <returns></returns>
        public async Task<int> CreateTicket(string title, string content, int entityId, int type, int category, List<int> assignedGroup, int? requesterGroup) {
            var action = GLPIAction.Ticket;

            // body
            dynamic input = new ExpandoObject();
            input.name = title;
            input.status = (int)TicketStatus.New;
            input.content = content;
            input.entities_id = entityId;
            input.urgency = (int)TicketScale.Average;
            input.impact = (int)TicketScale.Average;
            input.priority = (int)TicketScale.Average;
            input.itilcategories_id = category;
            input.type = type;

            if (requesterGroup.HasValue) {
                input._groups_id_requester = requesterGroup.Value;
            }

            var request = new RestRequest(action.Value, Method.Post)
                .AddJsonBody(new {
                    input = input
                });

            var response = await MakeGlpiRequest<GlpiResponseDto>(request);

            if(assignedGroup.Any()) {
                foreach (var group in assignedGroup) {
                    await AddTicketGroup(response.Id, group, UserType.AssignTo);
                }
            }

            return response.Id;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<int> AddTicketFollowup(int ticketId, string message) {
            var (url, json) = FollowUp932(ticketId, message);

            var request = new RestRequest(url, Method.Post)
                .AddJsonBody(json);

            var response = await MakeGlpiRequest<GlpiResponseDto>(request);

            return response.Id;
        }


        /// <summary>
        /// For GLPI 9.3.2
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="message"></param>
        /// <param name="solution"></param>
        /// <remarks>GLPI TicketFollowUp is deprecated from version 9.5.5. Please use ITILFollowup.</remarks>
        /// <returns></returns>
        private static (string url, object payload) FollowUp932(int ticketId, string message) {
            return ($"Ticket/{ticketId}/TicketFollowup",
                new {
                    input = new {
                        tickets_id = ticketId,
                        content = message,
                    }
                }
            );
        }

        /// <summary>
        /// For Glpi 9.5.5
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="message"></param>
        /// /// <remarks>GLPI TicketFollowUp 9.5.5 uses ITILFollowup.</remarks>
        /// <returns></returns>
        //private static (string url, object payload) FollowUp955(int ticketId, string message) {
        //    return ($"ITILFollowup",
        //        new {
        //            input = new {
        //                itemtype = "Ticket",
        //                items_id = ticketId,
        //                content = message,
        //                is_private = 0,
        //                requesttypes_id = 2
        //            }
        //        }
        //    );
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<int> AddTicketSolution(int ticketId, string message) {
            var action = GLPIAction.ITILSolution;

            var request = new RestRequest(action.Value, Method.Post)
                .AddJsonBody(new {
                    input = new {
                        itemtype = "Ticket",
                        items_id = ticketId,
                        solutiontypes_id = 2,
                        content = message,
                        status = 3
                    }
                });

            var response = await MakeGlpiRequest<GlpiResponseDto>(request);

            return response.Id;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<int> AddTicketDocument(int ticketId, IFormFile document) {
            using var fileContents = new MemoryStream();
            document.CopyTo(fileContents);

            // sanitize filename
            var extension = Path.GetExtension(document.FileName);
            var filename = document.FileName.Replace("€", "").Replace("/","");

            var manifest = JsonConvert.SerializeObject(new {
                input = new {
                    name = filename,
                    itemtype = "Ticket",
                    items_id = ticketId,
                    _filename = filename
                }
            });
           
            var request = new RestRequest($"ticket/{ticketId}/document", Method.Post)
                .AddQueryParameter("uploadManifest", manifest)
                .AddFile("_filename", fileContents.ToArray(), filename, document.ContentType);

            var response = await MakeGlpiRequest<GlpiResponseDto>(request);

            return response.Id;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public async Task<List<TicketUserDto>> GetTicketUsers(int ticketId) {
            var action = GLPIAction.Ticket;
            var url = $"{action.Value}/{ticketId}/Ticket_User";

            var request = new RestRequest(url, Method.Get);

            return await MakeGlpiRequest<List<TicketUserDto>>(request);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserDto> GetUser(int userId) {
            var action = GLPIAction.User;
            var url = $"{action.Value}/{userId}";

            var request = new RestRequest(url, Method.Get);

            return await MakeGlpiRequest<UserDto>(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<DocumentDto> GetDocument(int documentId) {
            var action = GLPIAction.Document;
            var url = $"{action.Value}/{documentId}";

            var request = new RestRequest(url, Method.Get)
                .AddHeader("Content-Type", "application/json");

            return await MakeGlpiRequest<DocumentDto>(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<byte[]> DownloadDocument(int documentId) {
            var action = GLPIAction.Document;
            var url = $"{action.Value}/{documentId}?alt=media";

            var request = new RestRequest(url, Method.Get);

            return await MakeGlpiRequestRaw(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public async Task<int> AddTicketUser(int ticketId, int userId, UserType userType) {
            var action = GLPIAction.Ticket;
            var url = $"{action.Value}/{ticketId}/Ticket_User";

            var request = new RestRequest(url, Method.Post)
                .AddJsonBody(new {
                    input = new {
                        tickets_id = ticketId,
                        users_id = userId,
                        type = userType.Value
                    }
                });

            var response = await MakeGlpiRequest<GlpiResponseDto>(request);

            return response.Id;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="groupId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public async Task<int> AddTicketGroup(int ticketId, int groupId, UserType userType) {
            var action = GLPIAction.Ticket;
            var url = $"{action.Value}/{ticketId}/group_ticket";

            var request = new RestRequest(url, Method.Post)
                .AddJsonBody(new {
                    input = new {
                        tickets_id = ticketId,
                        groups_id = groupId,
                        type = userType.Value
                    }
                });

            var response = await MakeGlpiRequest<GlpiResponseDto>(request);

            return response.Id;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<GroupDto> GetGroup(int groupId) {
            var action = GLPIAction.Group;
            var url = $"{action.Value}/{groupId}";

            var request = new RestRequest(url, Method.Get);

            return await MakeGlpiRequest<GroupDto>(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<TicketDto>> GetUserTickets(int userId) {
            var action = GLPIAction.User;
            var url = $"{action.Value}/{userId}/Ticket";

            var request = new RestRequest(url, Method.Get);

            return await MakeGlpiRequest<List<TicketDto>>(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<List<TicketDto>> GetGroupTickets(int groupId) {
            var action = GLPIAction.Group;
            var url = $"{action.Value}/{groupId}/Ticket";

            var request = new RestRequest(url, Method.Get);

            return await MakeGlpiRequest<List<TicketDto>>(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public async Task<List<FollowUpDto>> GetTicketFollowups(int ticketId) {
            var action = GLPIAction.TicketFollowUp;
            var url = $"ticket/{ticketId}/{action.Value}";

            var request = new RestRequest(url, Method.Get);

            return await MakeGlpiRequest<List<FollowUpDto>>(request);
        }


        public async Task<List<DocumentDto>> GetTicketDocuments(int ticketId) {
            var action = GLPIAction.Ticket;
            var url = $"{action.Value}/{ticketId}?with_documents=true";

            var request = new RestRequest(url, Method.Get);

            var list = await MakeGlpiRequest<TicketDocumentDto>(request);

            return list._documents;
        }


        public async Task ChangeTicketStatus(int ticketId, int statusId) {
            var action = GLPIAction.Ticket;
            var url = $"{action.Value}/{ticketId}";

            var request = new RestRequest(url, Method.Patch)
                .AddJsonBody(new {
                    input = new {
                        tickets_id = ticketId,
                        status = statusId
                    }
                });

            await MakeGlpiRequest(request);
        }


        public void Dispose() {
            _restClient?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
