using MediatR;

namespace GLPIService.Application.Tickets.Commands.AddDocument {

    public class AddDocumentCommand : IGlpiRequest, IRequest<int> {

        public int TicketId { get; set; }

        public IFormFile Document { get; set; } = null!;

    }
}
