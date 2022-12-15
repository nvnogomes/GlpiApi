using MediatR;

namespace GLPIService.Application.Documents.Commands.UploadDocument {

    public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, int> {

        private readonly IGlpiService _glpiService;

        public UploadDocumentCommandHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<int> Handle(UploadDocumentCommand request, CancellationToken cancellationToken) {

            return await _glpiService.AddTicketDocument(request.TicketId, request.Document);
        }
    }
}
