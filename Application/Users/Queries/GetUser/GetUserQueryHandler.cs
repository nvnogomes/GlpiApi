using MediatR;

namespace GLPIService.Application.Users.Queries.GetUser {

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto> {

        private readonly IGlpiService _glpiService;

        public GetUserQueryHandler(IGlpiService glpiService) {
            _glpiService = glpiService;
        }


        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken) {

            return await _glpiService.GetUser(request.UserId);
        }
    }
}
