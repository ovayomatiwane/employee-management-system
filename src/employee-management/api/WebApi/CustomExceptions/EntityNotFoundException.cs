namespace WebApi.CustomExceptions
{
    public class EntityNotFoundException : SystemException
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
