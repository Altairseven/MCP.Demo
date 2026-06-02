using MCP.Demo.Http.Models.Responses;

namespace MCP.Demo.Http;

public interface IBookingClient
{
    Task<List<ApartmentDto>> GetApartments(DateOnly from, DateOnly to, CancellationToken ct = default);
    Task<string> RegisterUser(string email, string firstName, string lastName, string password, CancellationToken ct = default);
    Task<string> BookApartment(string email, string password, Guid apartmentId, DateOnly from, DateOnly to, CancellationToken ct = default);
}
