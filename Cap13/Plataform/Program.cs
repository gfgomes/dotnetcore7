using Plataform;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("{first}/{second}/{third}", async context => {
    await context.Response.WriteAsync("Request Was Routed\n");
    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response
            .WriteAsync($"{kvp.Key}: {kvp.Value}\n");
    }
});

app.MapGet("files/{filename}.{ext}", async context => {
    await context.Response.WriteAsync("Request Was Routed\n");
    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response
            .WriteAsync($"{kvp.Key}: {kvp.Value}\n");
    }
});

app.MapGet("capital/{country}", Capital.Endpoint);
//app.MapGet("population/{city}", Population.Endpoint);
//app.MapGet("population/{city}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population"));
app.MapGet("size/{city}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population"));


app.Run();