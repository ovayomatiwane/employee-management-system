namespace WebApi.Models.Entities
{
    public class ConsultantTask
    {
        public required Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public Guid TaskId { get; set; }

        public Guid ConsultantId { get; set; }

        public DateTime? AssignedDate { get; set; }

        public int HoursCompleted { get; set; }

        public virtual Consultant Consultant { get; set; }

        public virtual Task Task { get; set; }

        public virtual Role Role { get; set; }
    }
}
