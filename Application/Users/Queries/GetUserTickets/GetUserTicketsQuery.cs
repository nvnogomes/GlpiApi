using MediatR;

namespace GLPIService.Application.Users.Queries.GetUserTickets {

    public class GetUserTicketsQuery : IGlpiRequest, IRequest<TicketsVm> {

        public int UserId { get; set; }
    }
}
