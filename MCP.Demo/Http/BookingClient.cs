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

    public async Task<string> RegisterUser(string email, string firstName, string lastName, string password, CancellationToken ct = default)
    {
        var userRegistration = await _bookingApi.RegisterUserAsync(new RegisterUserRequest(email, firstName, lastName, password));
        
        if (!userRegistration.IsSuccessStatusCode)
            throw new Exception("Unable to get apartments");

        return userRegistration.Content.ToString();
    }

    public async Task<string> BookApartment(string email, string password, Guid apartmentId, DateOnly from, DateOnly to, CancellationToken ct = default)
    {
        var tokenResponse = await _bookingApi.LoginUserAsync(new LogInUserRequest(email, password));
        var token = "Bearer " + (tokenResponse.IsSuccessStatusCode
            ? tokenResponse.Content?.AccessToken
            : throw new Exception("Unable to get apartments"));

        var booking = await _bookingApi.CreateBookingAsync(token, new ReserveBookingRequest(apartmentId, from, to));

        if (!booking.IsSuccessStatusCode)
            throw new Exception("Unable to get apartments");

        return booking.Content.ToString();
    }
}
