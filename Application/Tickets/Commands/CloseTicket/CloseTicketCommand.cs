using MediatR;

namespace GLPIService.Application.Tickets.Commands.CloseTicket {

    public class CloseTicketCommand : IGlpiRequest, IRequest<int> {

        public int TicketId { get; set; }

        public string Content { get; set; } = "";

    }
}
