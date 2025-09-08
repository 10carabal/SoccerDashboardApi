using FootballStats.Domain.Interfaces;
using FootballStats.Infrastructure.Services;
using FootballStats.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IFootballApiService, FootballApiService>(client =>
{
    client.BaseAddress = new Uri("https://v3.football.api-sports.io/");
});

// 1. Agregar servicio de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // origen de tu Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

//Casos de uso
builder.Services.AddScoped<GetTopScorerHandler>();
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");

app.MapControllers();


app.Run();
