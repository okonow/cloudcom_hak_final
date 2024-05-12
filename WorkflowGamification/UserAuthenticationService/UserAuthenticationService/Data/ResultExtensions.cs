using Microsoft.AspNetCore.Identity;
using UserAuthenticationService.Models;

namespace UserAuthenticationService.Data
{
    public static class ResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result, Guid userId)
        {
            return result.Succeeded
                 ? Result.Success(userId)
                 : Result.Failure(userId, result.Errors.Select(e => e.Description));
        }

        public static Result ToApplicationResult(Guid userId, bool succeeded)
        {
            return new Result(userId, succeeded, []);
        }
    }
}
