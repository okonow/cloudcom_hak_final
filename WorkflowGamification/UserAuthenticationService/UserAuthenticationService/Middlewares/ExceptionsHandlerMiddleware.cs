using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserAuthenticationService.Common.Exceptions;

namespace UserAuthenticationService.Middlewares
{
    public class ExceptionsHandlerMiddleware : IExceptionHandler
    {

        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

        public ExceptionsHandlerMiddleware()
        {
            _exceptionHandlers = new()
            {
                { typeof(NullEntityException), HandleNullEntityException },
                { typeof(ExistEntityException), HandleExistEntityException },
                { typeof(InvalidOperationException), HandleInvalidOperationException }
            };
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionType = exception.GetType();

            if (_exceptionHandlers.TryGetValue(exceptionType, out Func<HttpContext, Exception, Task>? value))
            {
                await value.Invoke(httpContext, exception);
                return true;
            }

            return false;
        }

        private async Task HandleNullEntityException(HttpContext context, Exception exception)
        {
            var ex = (NullEntityException)exception;

            context.Response.StatusCode = StatusCodes.Status404NotFound;

            await context.Response.WriteAsJsonAsync(new ValidationProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Type = ex.Message
            });
        }

        private async Task HandleExistEntityException(HttpContext context, Exception exception)
        {
            var ex = (ExistEntityException)exception;

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(new ValidationProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = ex.Message
            });
        }

        private async Task HandleInvalidOperationException(HttpContext context, Exception exception)
        {
            var ex = (InvalidOperationException)exception;

            context.Response.StatusCode = StatusCodes.Status404NotFound;

            await context.Response.WriteAsJsonAsync(new ValidationProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Type = ex.Message
            });
        }
    }
}
