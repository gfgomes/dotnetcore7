var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) => {
    await next();
    await context.Response
        .WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
});

//Custom Middleware
app.Use(async (context, next) => {
    if (context.Request.Method == HttpMethods.Get
            && context.Request.Query["custom"] == "true")
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Custom Middleware \n");
    }
    await next();
});

//Class Middleware 
app.UseMiddleware<Platform.QueryStringMiddleWare>();


//Hello World!
app.MapGet("/", () => "Hello World!");

app.Run();
