using Platform;
using Microsoft.AspNetCore.HttpLogging;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(opts => {
    opts.LoggingFields = HttpLoggingFields.RequestMethod
    | HttpLoggingFields.RequestPath
    | HttpLoggingFields.ResponseStatusCode;
});
var app = builder.Build();
app.UseHttpLogging();
app.UseStaticFiles();
app.MapGet("population/{city?}", Population.Endpoint);
app.Run();