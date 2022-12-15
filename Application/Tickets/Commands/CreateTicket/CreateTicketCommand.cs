using MediatR;

namespace GLPIService.Application.Commands.CreateTicket {

    public class CreateTicketCommand : IGlpiRequest, IRequest<int> {

        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public int Type { get; set; }
        public int Category { get; set; }
        public int? RequesterGroup { get; set; }
        public List<int> AssignedToGroup { get; set; } = new List<int>();
        public int EntityId { get; set; }

    }
}
