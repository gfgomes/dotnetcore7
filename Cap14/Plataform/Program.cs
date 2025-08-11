using Platform;
using Platform.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();


var app = builder.Build();

app.UseMiddleware<WeatherMiddleware>();

//app.MapGet("endpoint/class", WeatherEndpoint.Endpoint);
app.MapEndpoint<WeatherEndpoint>("endpoint/class");

//IResponseFormatter formatter = TextResponseFormatter.Singleton;
//IResponseFormatter formatter = TypeBroker.Formatter;

//app.MapGet("endpoint/function", async context => {
//    await formatter.Format(context,
//        "Endpoint Function: It is sunny in LA");
//});

app.MapGet("endpoint/function",
    async (HttpContext context, IResponseFormatter formatter) => {
        await formatter.Format(context,
            "Endpoint Function: It is sunny in LA");
    });

app.Run();