using MCP.Demo.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Refit;
using Scalar.AspNetCore;
using Microsoft.Extensions.Caching.Hybrid;

var builder = WebApplication.CreateBuilder(args);

// Register HybridCache
builder.Services.AddHybridCache();

builder.Services.AddOpenApi();

// Register BookingsConfiguration
builder.Services.Configure<BookingsConfiguration>(
    builder.Configuration.GetSection(nameof(BookingsConfiguration)));

// Register Refit client
builder.Services
    .AddRefitClient<IBookingApi>()
    .ConfigureHttpClient((sp, client) =>
    {
        var config = sp.GetRequiredService<IOptions<BookingsConfiguration>>();
        client.BaseAddress = new Uri(config.Value.BaseUrl);
    });

builder.Services.AddScoped<IBookingClient, BookingClient>();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithHttpTransport()
    .WithToolsFromAssembly()
    .WithPromptsFromAssembly();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// Map MCP with "mcp" prefix
app.MapMcp("mcp");

app.MapGet("apartments", async ([FromServices] IBookingClient service, CancellationToken ct) =>
{
    return Results.Ok(await service.GetApartments(DateOnly.MinValue, DateOnly.MaxValue, ct));
});

app.Run();

