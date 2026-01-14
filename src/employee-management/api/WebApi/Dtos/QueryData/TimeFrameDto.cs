namespace WebApi.Dtos.QueryData
{
    public class TimeFrameDto
    {
        public DateTime? TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        public Guid ConsultantId { get; set; }
    }
}
