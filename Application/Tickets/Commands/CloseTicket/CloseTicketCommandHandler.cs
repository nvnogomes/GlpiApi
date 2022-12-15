using MediatR;

namespace GLPIService.Application.Tickets.Commands.CloseTicket {

    public class CloseTicketCommandHandler : IRequestHandler<CloseTicketCommand, int> {

        private readonly IGlpiService _glpiService;

        public CloseTicketCommandHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<int> Handle(CloseTicketCommand request, CancellationToken cancellationToken) {

            return await _glpiService.AddTicketSolution(request.TicketId, request.Content);
        }
    }
}
