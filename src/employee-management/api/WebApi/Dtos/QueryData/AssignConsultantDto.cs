namespace WebApi.Dtos.QueryData
{
    public class AssignConsultantDto
    {
        public required Guid ConsultantId { get; set; }

        public required Guid TaskId { get; set; }

        public Guid RoleId { get; set; }

        public string? RoleName { get; set; }

        public float HourlyRate { get; set; }

        public int HoursCompleted { get; set; } = 0;
    }
}
