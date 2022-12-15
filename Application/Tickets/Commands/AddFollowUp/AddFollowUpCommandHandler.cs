using MediatR;

namespace GLPIService.Application.Tickets.Commands.AddFollowUp {

    public class AddFollowUpCommandHandler : IRequestHandler<AddFollowUpCommand, int> {

        private readonly IGlpiService _glpiService;

        public AddFollowUpCommandHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<int> Handle(AddFollowUpCommand request, CancellationToken cancellationToken) {

            return await _glpiService.AddTicketFollowup(request.TicketId, request.Content);
        }
    }
}
