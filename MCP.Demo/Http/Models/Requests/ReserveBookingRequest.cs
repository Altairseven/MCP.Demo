namespace MCP.Demo.Http.Models.Requests;

public record ReserveBookingRequest(Guid ApartmentId, DateOnly StartDate, DateOnly EndDate);
