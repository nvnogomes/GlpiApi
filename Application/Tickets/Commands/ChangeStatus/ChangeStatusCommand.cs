using MediatR;

namespace GLPIService.Application.Tickets.Commands.ChangeStatus {
    
    public class ChangeStatusCommand : IGlpiRequest, IRequest {

        public int TicketId { get; set; }
        public TicketStatus Status { get; set; }

    }
}
