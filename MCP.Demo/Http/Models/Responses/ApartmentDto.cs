namespace MCP.Demo.Http.Models.Responses;

public class ApartmentDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public string? Currency { get; set; }
    public AddressResponse? Address { get; set; }
}