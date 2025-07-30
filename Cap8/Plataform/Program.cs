using Microsoft.Extensions.Options;
using Plataform;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MessageOptions>(options => {
    options.CityName = "Albany";
});

var app = builder.Build();

app.MapGet("/location", async (HttpContext context,
    IOptions<MessageOptions> msgOpts) => {
        Plataform.MessageOptions opts = msgOpts.Value;
        await context.Response.WriteAsync($"{opts.CityName}, "
            + opts.CountryName);
    });

app.MapGet("/", () => "Hello World!");

app.Run();