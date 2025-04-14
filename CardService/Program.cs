using CardService.Infrastructure.HttpClients;
using CardService.Services.AllowedActionRules;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json;
using CardService.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
        });
    builder.Services.AddScoped<ICardDetailsClient, CardDetailsClient>();
    builder.Services.AddScoped<ICardAllowedActionsChooser, CardAllowedActionsChooser>();
    builder.Services.RegisterCardAllowedActions();
    builder.Services.AddScoped<ICardService, CardService.Services.CardService>();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient<ICardDetailsClient, CardDetailsClient>((sp, httpClient) =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        var baseAddress = config["CardDetailsClient:BaseAddress"];
        httpClient.BaseAddress = new Uri(baseAddress);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    })
        .AddPolicyHandler(HttpClientsPolicy.GetRetryPolicy());

    Log.Logger = new LoggerConfiguration()
        .WriteTo.File("logs/api.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.Host.UseSerilog();


}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.MapControllers();
    app.UseMiddleware<LoggingMiddleware>();

}

app.Run();