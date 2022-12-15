using GLPIService.Application.Common.Dtos;
using GLPIService.Application.Common.Vms;
using GLPIService.Application.Groups.Queries.GetGroup;
using GLPIService.Application.Groups.Queries.GetGroupTickets;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GLPIService.Controllers.V1 {

    [ApiVersion("1.0")]
    public class GroupController : ApiController {

        [HttpGet("{groupId}")]
        public async ValueTask<ActionResult<GroupDto>> Get(int groupId)
            => Ok(await Mediator.Send(new GetGroupQuery { GroupId = groupId }));



        [HttpGet("{groupId}/tickets")]
        public async ValueTask<ActionResult<TicketsVm>> GetTickets(int groupId)
            => Ok(await Mediator.Send(new GetGroupTicketsQuery { GroupId = groupId }));

    }
}
