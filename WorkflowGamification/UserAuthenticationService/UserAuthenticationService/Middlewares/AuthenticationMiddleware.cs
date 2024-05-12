using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace UserAuthenticationService.Middlewares
{
    public class AuthenticationMiddleware : IAuthorizationMiddlewareResultHandler
    {
        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            if (policy == null)
            {
                await next(context);
                return;
            }

            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                await AddUnauthorizedResponseToContextAsync(context);
            }

            await next(context);
        }
        private static async Task AddUnauthorizedResponseToContextAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized
            });
        }
    }
}
