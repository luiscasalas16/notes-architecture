using NCA.Common.Domain.Models;

namespace NCA.Common.Scheduler.Jobs
{
    public static class ApiRestErrors
    {
        public static string BaseCode => "Scheduler";

        public static Error JobNotFound() => new($"{BaseCode}.{"JobNotFound"}", "Job not found.");

        public static Error JobAlreadyStatus(string status) => new($"{BaseCode}.{"JobAlreadyStatus"}", $"Job is already {status}.");

        public static Error InvalidState() =>
            new(
                $"{BaseCode}.{"InvalidState"}",
                "Jobs in scheduler has diferent state of jobs in database. It is probable that the database has been directly updated or that there is a bug in the scheduler. You must restart the scheduler."
            );
    }
}
