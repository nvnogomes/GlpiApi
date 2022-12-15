using MediatR;

namespace GLPIService.Application.Tickets.Commands.AddFollowUp {

    public class AddFollowUpCommand : IGlpiRequest, IRequest<int> {

        public int TicketId { get; set; }

        public string Content { get; set; } = "";
    }
}
