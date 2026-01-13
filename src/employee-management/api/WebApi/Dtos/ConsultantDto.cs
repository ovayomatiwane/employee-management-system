namespace WebApi.Dtos;

public class ConsultantDto
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
