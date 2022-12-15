using MediatR;

namespace GLPIService.Application.Tickets.Commands.AddTicketGroup {

    public class AddTicketGroupCommandHandler : IRequestHandler<AddTicketGroupCommand, int> {

        private readonly IGlpiService _glpiService;

        public AddTicketGroupCommandHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<int> Handle(AddTicketGroupCommand request, CancellationToken cancellationToken) {

            var userType = UserType.GetByValue(request.TypeId);
            return await _glpiService.AddTicketGroup(request.TicketId, request.GroupId, userType);
        }
    }
}
