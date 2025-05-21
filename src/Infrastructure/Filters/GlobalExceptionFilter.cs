using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not BusinessException exception) return;

        var validation = new
        {
            Status = 400,
            Title = "Bad Request",
            Detail = exception?.Message
        };

        var json = new
        {
            errors = new[] { validation }
        };

        context.Result = new BadRequestObjectResult(json);
        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.ExceptionHandled = true;
    }
}