using GLPIService.Application.Commands.CreateTicket;
using GLPIService.Application.Common.Dtos;
using GLPIService.Application.Common.Enums;
using GLPIService.Application.Common.Vms;
using GLPIService.Application.Tickets.Commands.AddDocument;
using GLPIService.Application.Tickets.Commands.AddFollowUp;
using GLPIService.Application.Tickets.Commands.AddTicketGroup;
using GLPIService.Application.Tickets.Commands.AddTicketUser;
using GLPIService.Application.Tickets.Commands.ChangeStatus;
using GLPIService.Application.Tickets.Commands.CloseTicket;
using GLPIService.Application.Tickets.Queries.GetTicket;
using GLPIService.Application.Tickets.Queries.GetTicketDocuments;
using GLPIService.Application.Tickets.Queries.GetTicketFollowups;
using GLPIService.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLPIService.Controllers.V1 {

    [ApiVersion("1.0")]
    public class TicketController : ApiController {


        [HttpGet("{ticketId}")]
        public async ValueTask<ActionResult<TicketVm>> Get(int ticketId)
            => Ok(await Mediator.Send(new GetTicketQuery { TicketId = ticketId }));

        
        [HttpGet("{ticketId}/followups")]
        public async ValueTask<ActionResult<List<FollowUpDto>>> GetFollowups(int ticketId)
            => Ok(await Mediator.Send(new GetTicketFollowupsQuery { TicketId = ticketId }));

        [HttpGet("{ticketId}/documents")]
        public async ValueTask<ActionResult<List<DocumentDto>>> GetDocuments(int ticketId)
            => Ok(await Mediator.Send(new GetTicketDocumentsQuery { TicketId = ticketId }));


        [HttpPatch("{ticketId}/changestatus")]
        public async ValueTask<IActionResult> ChangeStatus(int ticketId, [FromQuery] int statusId)
            => Ok(await Mediator.Send(new ChangeStatusCommand { TicketId = ticketId, Status = (TicketStatus)statusId }));



        [HttpPost]
        public async ValueTask<ActionResult<int>> Create(CreateTicketCommand command) {
            var response = await Mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { ticketId = response }, response);
        }



        [HttpPost("{ticketId}/adddocument")]
        public async ValueTask<ActionResult<int>> AddDocument([FromForm] DocumentModel document, int ticketId)
            => Ok(await Mediator.Send(new AddDocumentCommand {
                TicketId = ticketId,
                Document = document.Document
            }));



        [HttpPost("{ticketId}/addfollowup")]
        public async ValueTask<ActionResult<int>> AddFollowUp(int ticketId, string content)
            => Ok(await Mediator.Send(new AddFollowUpCommand {
                TicketId = ticketId,
                Content = content
            }));



        [HttpPost("{ticketId}/adduser")]
        public async ValueTask<ActionResult<int>> AddUser(int ticketId, int userId, int typeId)
            => Ok(await Mediator.Send(new AddTicketUserCommand {
                TicketId = ticketId,
                UserId = userId,
                TypeId = typeId
            }));



        [HttpPost("{ticketId}/addgroup")]
        public async ValueTask<ActionResult<int>> AddGroup(int ticketId, int groupId, int typeId)
            => Ok(await Mediator.Send(new AddTicketGroupCommand {
                TicketId = ticketId,
                GroupId = groupId,
                TypeId = typeId
            }));



        [HttpPost("{ticketId}/close")]
        public async ValueTask<ActionResult<int>> Close(int ticketId, string content)
            => Ok(await Mediator.Send(new CloseTicketCommand {
                TicketId = ticketId,
                Content = content
            }));
    }
}
