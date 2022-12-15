using MediatR;

namespace GLPIService.Application.Groups.Queries.GetGroupTickets {

    public class GetGroupTicketsQuery : IGlpiRequest, IRequest<TicketsVm> {

        public int GroupId { get; set; }
    }
}
