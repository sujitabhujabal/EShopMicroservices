namespace Catalog.API.Exceptions
{
    public class NotFoundMessage : Exception
    {
        public NotFoundMessage(string message): base(message)
        { }    
    }
}
