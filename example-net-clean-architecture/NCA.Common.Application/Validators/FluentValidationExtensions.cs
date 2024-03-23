using FluentValidation;
using NCA.Common.Domain.Models;

namespace NCA.Common.Application.Validators
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string errorCode, string errorMessage)
        {
            return rule.WithErrorCode(errorCode).WithMessage(errorMessage);
        }

        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Error error)
        {
            return rule.WithErrorCode(error.Code).WithMessage(error.Message);
        }
    }
}
