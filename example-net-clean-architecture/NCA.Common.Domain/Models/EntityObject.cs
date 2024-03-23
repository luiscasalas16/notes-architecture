namespace NCA.Common.Domain.Models
{
    public abstract class EntityObject
    {
        protected static bool IsValid(FluentValidation.Results.ValidationResult result, out List<Error> errors)
        {
            if (result.IsValid)
            {
                errors = [];
                return true;
            }
            else
            {
                errors = result.Errors.Select(failure => new Error(failure.PropertyName, failure.ErrorCode, failure.ErrorMessage)).ToList();
                return false;
            }
        }
    }
}
