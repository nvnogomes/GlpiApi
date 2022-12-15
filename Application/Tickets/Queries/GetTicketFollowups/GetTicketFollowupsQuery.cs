using MediatR;

namespace GLPIService.Application.Tickets.Queries.GetTicketFollowups {

    public class GetTicketFollowupsQuery : IGlpiRequest, IRequest<List<FollowUpDto>> {

        public int TicketId { get; set; }
    }
}
