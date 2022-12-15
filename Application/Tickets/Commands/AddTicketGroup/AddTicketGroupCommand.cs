using MediatR;

namespace GLPIService.Application.Tickets.Commands.AddTicketGroup {

    public class AddTicketGroupCommand : IGlpiRequest, IRequest<int> {

        public int TicketId { get; set; }

        public int GroupId { get; set; }

        public int TypeId { get; set; }

    }
}
