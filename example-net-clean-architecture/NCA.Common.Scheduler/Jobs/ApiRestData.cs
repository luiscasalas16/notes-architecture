using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCA.Common.Scheduler.Jobs
{
    [Table("SCHEDULER_JOB_API_REST")]
    public class ApiRestData
    {
        [Key]
        [Column("SCHEDULER_JOB_API_REST_ID")]
        public int Id { get; set; }

        [Column("CODE")]
        public required string Code { get; set; }

        [Column("ENDPOINT")]
        public required string Endpoint { get; set; }

        [Column("SCHEDULE")]
        public required string Schedule { get; set; }

        [Column("STATUS")]
        public required string Status { get; set; }
    }
}
