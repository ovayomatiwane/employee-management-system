namespace WebApi.Dtos
{
    public class TaskDto
    {
        public required Guid Id { get; set; }

        public string? TaskName { get; set; }

        public string? Description { get; set; }

        public int MaxDuration { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
