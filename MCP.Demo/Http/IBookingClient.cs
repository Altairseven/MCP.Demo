using MCP.Demo.Http.Models.Responses;

namespace MCP.Demo.Http;

public interface IBookingClient
{
    Task<List<ApartmentDto>> GetApartments(DateOnly from, DateOnly to, CancellationToken ct = default);
}
