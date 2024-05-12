namespace UserAuthenticationService.Models
{
    public class Result
    {
        public Result(Guid userId, bool succeeded, IEnumerable<string> errors)
        {
            UserId = userId;
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public Guid UserId { get; init; }

        public bool Succeeded { get; init; }

        public string[] Errors { get; init; }

        public static Result Success(Guid userId)
        {
            return new Result(userId, true, []);
        }

        public static Result Failure(Guid userId, IEnumerable<string> errors)
        {
            return new Result(userId, false, errors);
        }
    }
}
