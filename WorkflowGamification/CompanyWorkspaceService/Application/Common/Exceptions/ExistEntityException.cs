namespace Application.Common.Exceptions
{
    public class ExistEntityException(string options) : Exception($"{options} already exist")
    {
    }
}
