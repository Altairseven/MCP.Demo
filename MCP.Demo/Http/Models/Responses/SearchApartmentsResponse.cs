namespace MCP.Demo.Http.Models.Responses;

public class SearchApartmentsResponse
{
    public IEnumerable<ApartmentDto>? Apartments { get; set; }
}