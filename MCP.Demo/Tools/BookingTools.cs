using MCP.Demo.Http;
using Microsoft.Extensions.Caching.Hybrid;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;
using ToonSharp;

namespace MCP.Demo.Tools;

[McpServerToolType, Description("Helps interact with the bookings API, allowing to book apartments for vacacions.")]
public class BookingTools
{
    private readonly IBookingClient _bookingClient;
    private readonly HybridCache _cache;

    public BookingTools(IBookingClient bookingClient, HybridCache cache)
    {
        _bookingClient = bookingClient;
        _cache = cache;
    }

    [McpServerTool(Name = "get_apartments"), Description("returns a list of apartments that are available for booking between two given dates and filtering by country")]
    public async Task<string> GetApartments(DateOnly from, DateOnly to, string Country)
    {
        var apartments = await _bookingClient.GetApartments(from, to, CancellationToken.None);

        var filtered = apartments
            .Where(x => x.Address?.Country != null && x.Address.Country.Equals(Country, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return ToonSerializer.Serialize(filtered);
    }

    [McpServerTool(Name = "register_user"), Description("given a first name, last name, email and password, it registers a user in the bookings api and returns the user id")]
    public async Task<string> GetApartments(string firstName, string lastName, string email, string password)
    {
        var userId = await _bookingClient.RegisterUser(email, firstName, lastName, password, CancellationToken.None);

        await _cache.SetAsync<string[]>($"user_{userId}", [email, password], options: new HybridCacheEntryOptions { Expiration = TimeSpan.FromDays(3) });
        
        return userId;
    }

    [McpServerTool(Name = "book_apartment"), Description("given a userId, apartmentId, from and to dates, it creates a booking for that apartment")]
    public async Task<string> GetApartments(Guid userId, Guid apartmentId, DateOnly from, DateOnly to)
    {
        var user = await _cache.GetOrCreateAsync<string[]>($"user_{userId}", (x)=> throw new ArgumentException("user not found in cache"));

        var booking = await _bookingClient.BookApartment(user[0], user[1], apartmentId, from, to, CancellationToken.None);

        return booking;
    }
}
