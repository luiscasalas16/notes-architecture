using NCA.Common.Domain.Models;

namespace NCA.Common.Application.Exceptions
{
    public class FailureException : ApplicationException
    {
        public List<Error> Errors { get; }

        public FailureException(Error error)
            : base()
        {
            Errors = [error];
        }

        public FailureException(IEnumerable<Error> errors)
        {
            Errors = errors.ToList();
        }
    }
}
