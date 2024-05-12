using System.Runtime.Serialization;

namespace UserAuthenticationService.Common.Exceptions
{
    public class NullEntityException(string entity) : Exception($"{entity} does not exist.")
    {
    }
}