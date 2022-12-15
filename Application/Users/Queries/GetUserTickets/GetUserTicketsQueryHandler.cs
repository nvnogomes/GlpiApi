using MediatR;

namespace GLPIService.Application.Users.Queries.GetUserTickets {

    public class GetUserTicketsQueryHandler : IRequestHandler<GetUserTicketsQuery, TicketsVm> {

        private readonly IGlpiService _glpiService;

        public GetUserTicketsQueryHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }



        public async Task<TicketsVm> Handle(GetUserTicketsQuery request, CancellationToken cancellationToken) {

            return new TicketsVm {
                Tickets = await _glpiService.GetUserTickets(request.UserId)
            };
        }
    }
}
