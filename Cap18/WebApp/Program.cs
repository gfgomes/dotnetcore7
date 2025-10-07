using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration[
        "ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

builder.Services.AddControllers();

builder.Services.AddRateLimiter(opts =>
{
    opts.AddFixedWindowLimiter("fixedWindow", fixOpts =>
    {
        fixOpts.PermitLimit = 1;
        fixOpts.QueueLimit = 0;
        fixOpts.Window = TimeSpan.FromSeconds(15);
    });
});

builder.Services.Configure<JsonOptions>(opts =>
{
    opts.JsonSerializerOptions.DefaultIgnoreCondition
        = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.UseRateLimiter(); // Middleware obrigatório para aplicar as políticas de limitação de taxa

// Aplica a todos os controladores a política "fixedWindow"
// Esta política pode ser substituída por controladores e ações EnableRateLimiting e DisableRateLimiting.
//app.MapControllers().RequireRateLimiting("fixedWindow");

// sem política global — aplicará só onde houver atributo
app.MapControllers();

app.MapGet("/", () => "Hello World!");

var context = app.Services.CreateScope().ServiceProvider
    .GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();