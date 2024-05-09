using FluentValidation.Results;
using NCA.Common.Domain.Models;

namespace NCA.Common.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<Error> Errors { get; }

        public ValidationException()
            : base()
        {
            Errors = [];
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures.Select(failure => new Error(failure.PropertyName, failure.ErrorCode, failure.ErrorMessage)).ToList();
        }
    }
}
