using MediatR;

namespace GLPIService.Application.Generics.Commands.GenericRequest {
    public class GenericRequestCommandHandler : IRequestHandler<GenericRequestCommand, string> {

        private readonly IGlpiService _glpiService;

        public GenericRequestCommandHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }

        public async Task<string> Handle(GenericRequestCommand request, CancellationToken cancellationToken) {

            return await _glpiService.GenericRequest(request.Method, request.Parameters);
        }
    }
}
