using MediatR;

namespace GLPIService.Application.Tickets.Queries.GetTicketDocuments {
    public class GetTicketDocumentsQuery : IGlpiRequest, IRequest<List<DocumentDto>> {

        public int TicketId { get; set; }
    }
}
