using NCA.Common.Domain.Models;

namespace NCA.Common.Application.Exceptions
{
    public class FailureException : ApplicationException
    {
        public List<Error> Errors { get; }

        public FailureException(Error error)
            : this(error, null!) { }

        public FailureException(IEnumerable<Error> errors)
            : this(errors, null!) { }

        public FailureException(Error error, Exception exception)
            : base(Error.ConvertToString(error), exception)
        {
            Errors = [error];
        }

        public FailureException(IEnumerable<Error> errors, Exception exception)
            : base(Error.ConvertToString(errors.ToList()), exception)
        {
            Errors = errors.ToList();
        }
    }
}
