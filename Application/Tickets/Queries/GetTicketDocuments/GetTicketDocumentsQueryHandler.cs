using MediatR;

namespace GLPIService.Application.Tickets.Queries.GetTicketDocuments {
    public class GetTicketDocumentsQueryHandler : IRequestHandler<GetTicketDocumentsQuery, List<DocumentDto>> {

        private readonly IGlpiService _glpiService;

        public GetTicketDocumentsQueryHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<List<DocumentDto>> Handle(GetTicketDocumentsQuery request, CancellationToken cancellationToken) {

            // get documents
            var documents = await _glpiService.GetTicketDocuments(request.TicketId);
            
            // get document users
            foreach (var doc in documents) {
                doc.User = await _glpiService.GetUser(doc.Users_Id);
            }

            return documents;
        }
    }
}
