namespace WebApi.Dtos.ResponseData
{
    public class TotalOwedDto
    {
        public DateTime? TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        public Guid ConsultantId { get; set; }

        public string? FullNames { get; set; }

        public float TotalOwed { get; set; }
    }
}
