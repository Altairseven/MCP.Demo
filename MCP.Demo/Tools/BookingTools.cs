using MCP.Demo.Http;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MCP.Demo.Tools;

[McpServerToolType, Description("Helps interact with the bookings API, allowing to book apartments for vacacions.")]
public class BookingTools
{
    private readonly IBookingClient _bookingClient;

    public BookingTools(IBookingClient bookingClient)
    {
        _bookingClient = bookingClient;
    }

    [McpServerTool(Name = "get_apartments"), Description("returns a list of apartments that are available for booking between two given dates")]
    public async Task<string> GetApartments(DateOnly from, DateOnly to)
    {
        var apartments = await _bookingClient.GetApartments(from, to, CancellationToken.None);

        return string.Join(", " ,apartments.Select(x => x.Name).ToList());
    }
}
