using WebApi.Models.Entities;

namespace WebApi.Dtos
{
    public class RoleRateDto
    {
        public required Guid Id { get; set; }

        public float HourlyRate { get; set; }

        public Guid RoleId { get; set; }

        public DateTime? AssignedDate { get; set; }

        public RoleDto Role { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
