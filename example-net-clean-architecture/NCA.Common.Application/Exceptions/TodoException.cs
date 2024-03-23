namespace NCA.Common.Application.Exceptions
{
    public class TodoException : ApplicationException
    {
        public TodoException(string message)
            : base(message) { }
    }
}
