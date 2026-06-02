namespace MCP.Demo.Http.Models.Requests;

public record RegisterUserRequest(string? Email, string? FirstName, string? LastName, string? Password);
