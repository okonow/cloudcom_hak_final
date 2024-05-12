
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWorkspaceService.Middlewares
{
    public class ExceptionsMiddleware : IExceptionHandler
    {
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

        public ExceptionsMiddleware()
        {
            _exceptionHandlers = new()
            {
                {typeof(NullEntityException), HandleNullEntityException },
                {typeof(ExistEntityException), HandleExistEntityException }
            }; 
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionType = exception.GetType();

            if (_exceptionHandlers.ContainsKey(exceptionType))
            {
                await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
                return true;
            }

            return false;
        }

        private async Task HandleNullEntityException(HttpContext context, Exception ex)
        {
            var exception = (NullEntityException)ex;

            context.Response.StatusCode = StatusCodes.Status404NotFound;

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = exception.Message
            });
        }

        private async Task HandleExistEntityException(HttpContext context, Exception ex)
        {
            var exception = (ExistEntityException)ex;

            context.Response.StatusCode = StatusCodes.Status409Conflict;

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = exception.Message
            });
        }
    }
}
