using NCA.Common.Domain.Models;

namespace NCA.Production.Domain.Errors
{
    public static class GenericErrors
    {
        private static string BaseCode => nameof(GenericErrors);

        public static Error NotFound(int id) => new($"{BaseCode}.{nameof(NotFound)}", $"The entity with the Id = '{id}' was not found.");

        public static Error NotNull => new($"{BaseCode}.{nameof(NotNull)}", "Cannot be null.");

        public static Error NotEmpty => new($"{BaseCode}.{nameof(NotEmpty)}", "Cannot be empty string.");
    }
}
