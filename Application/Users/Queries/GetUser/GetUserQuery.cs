using MediatR;

namespace GLPIService.Application.Users.Queries.GetUser {

    public class GetUserQuery : IGlpiRequest, IRequest<UserDto> {

        public int UserId { get; set; }

    }
}
