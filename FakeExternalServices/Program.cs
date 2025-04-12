using FakeExternalServices.Services;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            // Global settings: use the defaults, but serialize enums as strings
            // (because it really should be the default)
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
        });
    builder.Services.AddScoped<ICardDetailsService, CardDetailsService>();
    builder.Services.AddSingleton<ICardsRepository, CardsRepository>();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.MapControllers();
}

app.Run();
