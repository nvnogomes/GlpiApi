using MediatR;

namespace GLPIService.Application.Tickets.Commands.ChangeStatus {

    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, Unit> {

        private readonly IGlpiService _glpiService;

        public ChangeStatusCommandHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }

        public async Task<Unit> Handle(ChangeStatusCommand request, CancellationToken cancellationToken) {

            await _glpiService.ChangeTicketStatus(request.TicketId, (int)request.Status);

            return Unit.Value;
        }
    }
}
