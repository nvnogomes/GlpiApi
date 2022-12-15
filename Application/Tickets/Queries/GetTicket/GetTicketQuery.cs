using MediatR;

namespace GLPIService.Application.Tickets.Queries.GetTicket {

    public class GetTicketQuery : IGlpiRequest, IRequest<TicketVm> {

        public int TicketId { get; set; }

    }
}
