namespace UserAuthenticationService.Common.Exceptions
{
    public class ExistEntityException(string entity) : Exception($"{entity} already exist.")
    {
    }
}
