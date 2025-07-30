var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//http://localhost:5000/branch?custom=true
((IApplicationBuilder)app).Map("/branch", branch => {

    branch.UseMiddleware<Platform.QueryStringMiddleWare>();

    branch.Run(async (context) => {
        await context.Response.WriteAsync($"Branch Middleware");
    });
});


//http://localhost:5000/?custom=true
app.UseMiddleware<Platform.QueryStringMiddleWare>();

app.MapGet("/", () => "Hello World!");

app.Run();