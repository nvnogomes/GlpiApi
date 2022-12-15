using MediatR;

namespace GLPIService.Application.Documents.Commands.UploadDocument {
    
    
    public class UploadDocumentCommand : IGlpiRequest, IRequest<int> {
        public IFormFile Document { get; set; }
        public int TicketId { get; set; }
    }
}
