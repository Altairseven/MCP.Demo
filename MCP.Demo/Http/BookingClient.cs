using MCP.Demo.Http.Models.Requests;
using MCP.Demo.Http.Models.Responses;

namespace MCP.Demo.Http;

public class BookingClient : IBookingClient
{
    private readonly IBookingApi _bookingApi;

    public BookingClient(IBookingApi bookingApi)
    {
        _bookingApi = bookingApi;
    }

    public async Task<List<ApartmentDto>> GetApartments(DateOnly from, DateOnly to, CancellationToken ct = default)
    {
        var tokenResponse = await _bookingApi.LoginUserAsync(new LogInUserRequest("Test@test.com", "Test1234"));
        var token = "Bearer " + (tokenResponse.IsSuccessStatusCode
            ? tokenResponse.Content?.AccessToken
            : throw new Exception("Unable to get apartments"));

        var apartments = await _bookingApi.GetApartmentsAsync(token, from, to, ct);

        if (!apartments.IsSuccessStatusCode)
            throw new Exception("Unable to get apartments");

        return apartments.Content?.Apartments?.ToList() ?? [];
    }
}
