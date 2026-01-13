using WebApi.Models.Entities;

namespace WebApi.Dtos
{
    public class ConsultantTaskDto
    {
        public required Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public Guid TaskId { get; set; }

        public Guid ConsultantId { get; set; }

        public DateTime? AssignedDate { get; set; }

        public int HoursCompleted { get; set; }

        public ConsultantDto Consultant { get; set; }

        public TaskDto Task { get; set; }

        public RoleDto Role { get; set; }
    }
}
