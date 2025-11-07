namespace MCP.Demo.Http.Models.Requests;

public class ReserveBookingRequest
{
    public Guid ApartmentId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}