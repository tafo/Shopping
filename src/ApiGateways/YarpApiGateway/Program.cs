using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(rateLimiterOptions  =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        // We can send 5 requests in 10 seconds
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 2;
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRateLimiter();

app.MapReverseProxy();

app.Run();