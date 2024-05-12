namespace Application.Common.Exceptions
{
    public class NullEntityException(string options) : Exception($"{options} does not exist")
    {
    }
}
