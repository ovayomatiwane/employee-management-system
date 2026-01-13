namespace WebApi.Models.Entities
{
    public class ConsultantTask
    {
        public required Guid Id { get; set; }

        public Guid RoleRateId { get; set; }

        public Guid TaskId { get; set; }

        public Guid ConsultantId { get; set; }

        public DateTime? AssignedDate { get; set; }

        public int HoursCompleted { get; set; }

        public virtual Consultant Consultant { get; set; }

        public virtual Task Task { get; set; }

        public virtual RoleRate RoleRate { get; set; }
    }
}
