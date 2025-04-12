using CardService.Infrastructure.HttpClients;
using CardService.Services;
using CardService.Services.AllowedActionRules;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<ICardDetailsClient, CardDetailsClient>();
    builder.Services.AddScoped<ICardAllowedActionsChooser, CardAllowedActionsChooser>();
    builder.Services.AddScoped<ICardsService, CardsService>();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient();
    builder.Services.AddHttpClient<ICardDetailsClient, CardDetailsClient>(httpClient =>
    {
        httpClient.BaseAddress = new Uri("http://localhost:5149/");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    })
        .AddPolicyHandler(HttpClientsPolicy.GetRetryPolicy());
    builder.Services.RegisterCardAllowedActions();

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