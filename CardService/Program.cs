using CardService.Infrastructure.HttpClients;
using CardService.Services.AllowedActionRules;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Reflection;
using FluentValidation;
using CardService.Interceptors;
using CardService.UserCardsModule.Validators;
using CardService.UserCardsModule.Queries;
using FluentValidation.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
    })
   ;
builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddScoped<ICardDetailsClient, CardDetailsClient>();
builder.Services.AddScoped<ICardAllowedActionsChooser, CardAllowedActionsChooser>();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();
builder.Services.AddScoped<IValidator<CardActionsQuery>, CardActionsQueryValidator>();
builder.Services.RegisterCardAllowedActions();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ICardDetailsClient, CardDetailsClient>((sp, httpClient) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var baseAddress = config["CardDetailsClient:BaseAddress"];
    httpClient.BaseAddress = new Uri(baseAddress);
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
})
    .AddPolicyHandler(HttpClientsPolicy.GetRetryPolicy());


builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("CardService"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddPrometheusExporter();
    });

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/api.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();


var app = builder.Build();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<FluentValidationExceptionMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();
