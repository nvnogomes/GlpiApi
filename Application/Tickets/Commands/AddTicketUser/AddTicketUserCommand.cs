using MediatR;

namespace GLPIService.Application.Tickets.Commands.AddTicketUser {

    public class AddTicketUserCommand : IGlpiRequest, IRequest<int> {

        public int TicketId { get; set; }

        public int UserId { get; set; }

        public int TypeId { get; set; }


    }
}
