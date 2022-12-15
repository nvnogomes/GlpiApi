using GLPIService.Application.Generics.Commands.GenericRequest;
using GLPIService.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GLPIService.WebApi.Controllers.V1 {


    [ApiVersion("1.0")]
    public class GenericController : ApiController {

        [HttpPost]
        public async Task<ActionResult<string>> GenericPost(string method, [FromBody] object param)
            => Ok(await Mediator.Send(new GenericRequestCommand { Method = method, Parameters = param}));



    }
}
