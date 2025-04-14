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
using Prometheus;
using CardService.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<CardDetailsClientOptions>(builder.Configuration.GetSection("CardDetailsClient"));
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
    var options = sp.GetRequiredService<IOptions<CardDetailsClientOptions>>().Value;
    httpClient.BaseAddress = new Uri(options.BaseAddress);
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
})
    .AddPolicyHandler(HttpClientsPolicy.GetRetryPolicy());


builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("CardService"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter();
    })
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
    .WriteTo.File(builder.Configuration["Logging:FileName"] ?? string.Empty, rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

var cardRequestsCounter = Metrics.CreateCounter("card_requests_total", "Total number of requests to the card service.");
builder.Services.AddSingleton(cardRequestsCounter);

var app = builder.Build();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<FluentValidationExceptionMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics();
});

app.MapControllers();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.Run();
