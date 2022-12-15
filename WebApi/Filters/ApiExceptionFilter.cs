using GLPIService.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Linq;

namespace GLPIService.Filters {

    public class ApiExceptionFilter : ExceptionFilterAttribute {

        public override void OnException(ExceptionContext context) {
            var exceptionType = context.Exception.GetType();

            var errorMessage = "Unexpected error";
            switch (context.Exception) {
                case UnauthorizedAccessException _:
                    errorMessage = "Access to the Web API is not authorized.";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                case GlpiException _:
                    errorMessage = $"Glpi communication error: {context.Exception.Message}";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
                case NotFoundException _:
                    errorMessage = context.Exception.Message;
                    context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case ValidationException ve:
                    errorMessage = $"Validation errors: {ve.Errors.First().Value[0]}";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    break;
                default:
                    errorMessage = $"An error occurred. {context.Exception.Message}";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var apiError = new APIError(errorMessage);
            Log.Error(context.Exception, errorMessage);

            context.Result = new JsonResult(apiError);
            base.OnException(context);
        }
    }
}
