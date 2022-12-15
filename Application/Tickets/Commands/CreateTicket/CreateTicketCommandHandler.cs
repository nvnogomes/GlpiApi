using MediatR;

namespace GLPIService.Application.Commands.CreateTicket {
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int> {

        private readonly IGlpiService _glpiService;

        public CreateTicketCommandHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }

        public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken) {

            return await _glpiService.CreateTicket(request.Title,
                                                   request.Content,
                                                   request.EntityId,
                                                   request.Type,
                                                   request.Category,
                                                   request.AssignedToGroup,
                                                   request.RequesterGroup);
        }
    }
}
