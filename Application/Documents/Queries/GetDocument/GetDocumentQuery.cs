using MediatR;

namespace GLPIService.Application.Documents.Queries.GetDocument {

    public class GetDocumentQuery : IGlpiRequest, IRequest<ExportFileVm> {

        public int DocumentId { get; set; }

    }
}
