using MediatR;

namespace GLPIService.Application.Tickets.Queries.GetTicketFollowups {

    public class GetTicketFollowupsQueryHandler : IRequestHandler<GetTicketFollowupsQuery, List<FollowUpDto>> {

        private readonly IGlpiService _glpiService;

        public GetTicketFollowupsQueryHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<List<FollowUpDto>> Handle(GetTicketFollowupsQuery request, CancellationToken cancellationToken) {

            // get followups
            var followups = await _glpiService.GetTicketFollowups(request.TicketId);

            // get followup users
            foreach (var fups in followups) {
                fups.User = await _glpiService.GetUser(fups.Users_id);
            }

            return followups;
        }

    }
}
