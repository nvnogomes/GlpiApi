namespace GLPIService.Application.Common.Interfaces {

    public interface IGlpiService {


        /// <summary>
        /// Logs in the user in the glpi application
        /// Assigns the session token
        /// 
        /// GLPI returns:
        ///  200 (OK) with the session_token string.
        ///  400 (Bad Request) with a message indicating an error in input parameter.
        ///  401 (UNAUTHORIZED)
        /// </summary>
        Task Login();


        /// <summary>
        /// Kills the session with Glpi
        /// 
        /// GLPI Returns:
        ///  200 (OK).
        ///  400 (Bad Request) with a message indicating an error in input parameter.
        /// </summary>
        /// <returns></returns>
        Task Logout();


        /// <summary>
        /// Generic request
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<string> GenericRequest(string method, object? parameters);


        /// <summary>
        /// Gets the basic information of specified ticket 
        /// 
        /// GLPI Returns:
        /// 200 (OK) with item data(Last-Modified header should contain the date of last modification of the item).
        /// 401 (UNAUTHORIZED).
        /// 404 (NOT FOUND).
        /// </summary>
        /// <param name="ticketId">int glpiId</param>
        /// <returns>Ticket object</returns>
        Task<TicketDto> GetTicket(int ticketId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        Task<List<TicketUserDto>> GetTicketUsers(int ticketId);





        /// <summary>
        /// Creates a ticket item with the information given in the request object
        /// 
        /// GLPI Returns:
        /// 201 (OK) with id of added items.
        /// 207 (Multi-Status) with id of added items and errors.
        /// 400 (Bad Request) with a message indicating an error in input parameter.
        /// 401 (UNAUTHORIZED).
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns>GlpiResponse object</returns>
        Task<int> CreateTicket(string title, string content, int entityId, int type, int category, List<int> assignedGroup, int? requesterGroup);


        /// <summary>
        /// Creates a ticket item with the information given in the request object
        /// 
        /// GLPI Returns:
        /// 201 (OK) with id of added items.
        /// 207 (Multi-Status) with id of added items and errors.
        /// 400 (Bad Request) with a message indicating an error in input parameter.
        /// 401 (UNAUTHORIZED).
        /// </summary>
        /// <param name="tObject"></param>
        /// <returns>GlpiResponse object</returns>
        Task<int> AddTicketFollowup(int ticketId, string message);


        /// <summary>
        /// Creates a ticket item with the information given in the request object
        /// 
        /// GLPI Returns:
        /// 201 (OK) with id of added items.
        /// 207 (Multi-Status) with id of added items and errors.
        /// 400 (Bad Request) with a message indicating an error in input parameter.
        /// 401 (UNAUTHORIZED).
        /// </summary>
        /// <param name="tObject"></param>
        /// <returns>GlpiResponse object</returns>
        Task<int> AddTicketSolution(int ticketId, string message);



        /// <summary>
        /// Creates a ticket item with the information given in the request object
        /// 
        /// GLPI Returns:
        /// 201 (OK) with id of added items.
        /// 207 (Multi-Status) with id of added items and errors.
        /// 400 (Bad Request) with a message indicating an error in input parameter.
        /// 401 (UNAUTHORIZED).
        /// </summary>
        /// <param name="tObject"></param>
        /// <returns>GlpiResponse object</returns>
        Task<int> AddTicketDocument(int ticketId, IFormFile document);


        /// <summary>
        /// Add user to ticket with specific type
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        Task<int> AddTicketUser(int ticketId, int userId, UserType userType);



        /// <summary>
        /// Add group to ticket with specific type
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="groupId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        Task<int> AddTicketGroup(int ticketId, int groupId, UserType userType);



        /// <summary>
        /// Change the ticket status
        /// </summary>
        /// <param name="ticketId">Ticket to be changed</param>
        /// <param name="statusId">New status</param>
        Task ChangeTicketStatus(int ticketId, int statusId);



        /// <summary>
        /// Get user info
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserDto> GetUser(int userId);




        /// <summary>
        /// Get all ticket followups
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        Task<List<FollowUpDto>> GetTicketFollowups(int ticketId);


        /// <summary>
        /// Get all ticket followups
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        Task<List<DocumentDto>> GetTicketDocuments(int ticketId);




        /// <summary>
        /// Get all tickets created by the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<TicketDto>> GetUserTickets(int userId);


        /// <summary>
        /// Get group info
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<GroupDto> GetGroup(int groupId);


        /// <summary>
        /// Get all tickets created by the group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<List<TicketDto>> GetGroupTickets(int groupId);


        /// <summary>
        /// Get document info
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        Task<DocumentDto> GetDocument(int documentId);


        /// <summary>
        /// Download document
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        Task<byte[]> DownloadDocument(int documentId);
    }


}
