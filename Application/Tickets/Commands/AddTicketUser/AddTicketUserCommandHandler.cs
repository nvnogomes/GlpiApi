using MediatR;

namespace GLPIService.Application.Tickets.Commands.AddTicketUser {

    public class AddTicketUserCommandHandler : IRequestHandler<AddTicketUserCommand, int> {

        private readonly IGlpiService _glpiService;

        public AddTicketUserCommandHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<int> Handle(AddTicketUserCommand request, CancellationToken cancellationToken) {

            var userType = UserType.GetByValue(request.TypeId);
            return await _glpiService.AddTicketUser(request.TicketId, request.UserId, userType);
        }
    }
}
