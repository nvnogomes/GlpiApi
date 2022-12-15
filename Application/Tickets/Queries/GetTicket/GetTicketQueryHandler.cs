using MediatR;

namespace GLPIService.Application.Tickets.Queries.GetTicket {

    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, TicketVm> {

        private readonly IGlpiService _glpiService;

        public GetTicketQueryHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<TicketVm> Handle(GetTicketQuery request, CancellationToken cancellationToken) {

            // load ticket
            var ticket = await _glpiService.GetTicket(request.TicketId);

            
            // get document users
            var documents = await _glpiService.GetTicketDocuments(request.TicketId);
            foreach (var doc in documents) {
                doc.User = await _glpiService.GetUser(doc.Users_Id);
            }

            // followups
            var followups = await _glpiService.GetTicketFollowups(request.TicketId);
            foreach (var fups in followups) {
                fups.User = await _glpiService.GetUser(fups.Users_id);
            }

            return new TicketVm {
                Ticket = ticket,
                Documents = documents,
                Followups = followups
            };
        }


    }
}
