using MediatR;

namespace GLPIService.Application.Tickets.Commands.AddDocument {

    public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand, int> {

        private readonly IGlpiService _glpiService;

        public AddDocumentCommandHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<int> Handle(AddDocumentCommand request, CancellationToken cancellationToken) {

            return await _glpiService.AddTicketDocument(request.TicketId, request.Document);
        }
    }
}
