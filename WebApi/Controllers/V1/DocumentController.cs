using GLPIService.Application.Common.Vms;
using GLPIService.Application.Documents.Commands.UploadDocument;
using GLPIService.Application.Documents.Queries.GetDocument;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GLPIService.Controllers.V1 {

    [ApiVersion("1.0")]
    public class DocumentController : ApiController {


        [HttpGet("{id}")]
        public async ValueTask<ActionResult<ExportFileVm>> Get(int id) {
            var vm = await Mediator.Send(new GetDocumentQuery { DocumentId = id });

            return File(vm.Content, vm.ContentType, vm.FileName);
        }



        [HttpPost]
        public async ValueTask<ActionResult<int>> Create([FromForm] UploadDocumentCommand command)
            => Ok(await Mediator.Send(command));
    }
}
