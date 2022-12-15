using MediatR;

namespace GLPIService.Application.Groups.Queries.GetGroupTickets {

    public class GetGroupTicketsQueryHandler : IRequestHandler<GetGroupTicketsQuery, TicketsVm> {

        private readonly IGlpiService _glpiService;

        public GetGroupTicketsQueryHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }



        public async Task<TicketsVm> Handle(GetGroupTicketsQuery request, CancellationToken cancellationToken) {

            return new TicketsVm {
                Tickets = await _glpiService.GetGroupTickets(request.GroupId)
            };
        }
    }
}
