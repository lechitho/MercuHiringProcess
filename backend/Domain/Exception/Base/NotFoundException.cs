
namespace Domain.Exception.Base
{
    public class NotFoundException : IOException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
