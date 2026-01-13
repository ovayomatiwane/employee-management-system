namespace WebApi.Models.Entities
{
    public class Role
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public float HourlyRate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime RemovedDate { get; set; }
    }
}
