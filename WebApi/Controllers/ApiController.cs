using GLPIService.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GLPIService.Controllers {


    [ApiController]
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiController : ControllerBase {

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    }
}
