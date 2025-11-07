using MCP.Demo.Http.Models.Requests;
using MCP.Demo.Http.Models.Responses;
using Refit;

namespace MCP.Demo.Http;

public interface IBookingApi
{
    [Get("/api/apartments")]
    Task<IApiResponse<SearchApartmentsResponse>> GetApartmentsAsync(
        [Header("Authorization")] string authorization,
        DateOnly? startDate,
        DateOnly? endDate,
        CancellationToken cancellationToken = default);

    [Get("/api/bookings/{id}")]
    Task<IApiResponse<BookingResponse>> GetBookingAsync(
        [Header("Authorization")] string authorization,
        Guid id,
        CancellationToken cancellationToken = default);

    [Post("/api/bookings")]
    Task<IApiResponse<Guid>> CreateBookingAsync(
        [Header("Authorization")] string authorization,
        [Body] ReserveBookingRequest request,
        CancellationToken cancellationToken = default);

    [Post("/api/users/register")]
    Task<IApiResponse<Guid>> RegisterUserAsync(
        [Body] RegisterUserRequest request,
        CancellationToken cancellationToken = default);

    [Post("/api/users/login")]
    Task<IApiResponse<AccessTokenResponse>> LoginUserAsync(
        [Body] LogInUserRequest request,
        CancellationToken cancellationToken = default);
}
