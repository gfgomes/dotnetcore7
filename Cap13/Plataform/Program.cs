using Plataform;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseRouting();

//#pragma warning disable ASP0014

//app.UseEndpoints(endpoints => {
//    endpoints.MapGet("routing", async context => {
//        await context.Response.WriteAsync("Request Was Routed"); 
//    });


//    endpoints.MapGet("capital/uk", new Capital().Invoke);
//    endpoints.MapGet("population/paris", new Population().Invoke); 
//});

app.MapGet("routing", async context => {
    await context.Response.WriteAsync("Request Was Routed");
});
app.MapGet("capital/uk", new Capital().Invoke);
app.MapGet("population/paris", new Population().Invoke);
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Terminal Middleware Reached");
//});

app.Run();