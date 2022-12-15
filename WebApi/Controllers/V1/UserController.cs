using GLPIService.Application.Common.Dtos;
using GLPIService.Application.Common.Vms;
using GLPIService.Application.Users.Queries.GetUser;
using GLPIService.Application.Users.Queries.GetUserTickets;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GLPIService.Controllers.V1 {


    [ApiVersion("1.0")]
    public class UserController : ApiController {


        [HttpGet("{userId}")]
        public async ValueTask<ActionResult<UserDto>> Get(int userId)
            => Ok(await Mediator.Send(new GetUserQuery {
                UserId = userId
            }));


        [HttpGet("{userId}/tickets")]
        public async ValueTask<ActionResult<TicketsVm>> GetTickets(int userId)
           => Ok(await Mediator.Send(new GetUserTicketsQuery {
               UserId = userId
           }));


    }
}
