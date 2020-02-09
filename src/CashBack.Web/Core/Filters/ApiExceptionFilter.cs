using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using CashBack.Application.Common.Exceptions;

namespace CashBack.Web.Core.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            this.logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            ValidationProblemDetails apiError = null;
            if (context.Exception is BusinessValidationException ve)
            {
                context.Exception = null;
                apiError = new ValidationProblemDetails(ve.Failures)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details.",
                };

                context.HttpContext.Response.ContentType = "application/problem+json";
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (context.Exception is NotFoundException nfe)
            {
                apiError = new ValidationProblemDetails()
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = nfe.Message
                };
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (context.Exception is AlreadyExistsException aee)
            {
                apiError = new ValidationProblemDetails()
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = aee.Message
                };
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            }
            else
            {
                logger.LogError(context.Exception, context.Exception.Message);
#if !DEBUG
                var msg = "An unhandled error occurred.";
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                apiError = new ValidationProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = stack,
                    Title = msg
                };

                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.Result = new JsonResult(apiError);
            base.OnException(context);
        }
    }
}
