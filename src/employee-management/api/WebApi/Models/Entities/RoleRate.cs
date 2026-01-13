namespace WebApi.Models.Entities
{
    public class RoleRate
    {
        public required Guid Id { get; set; }

        public float HourlyRate { get; set; }

        public Guid RoleId { get; set; }

        public DateTime? AssignedDate { get; set; }

        public virtual Role Role { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
