using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;
using MuseumSystem.Infrastructure.InMemory.Repositories;
using MuseumSystem.Services.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Регистрация обобщенных репозиториев
builder.Services.AddSingleton<IRepository<Exhibition>>(sp =>
    new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions()));

builder.Services.AddSingleton<IRepository<Visitor>>(sp =>
    new InMemoryRepository<Visitor>(MuseumDataSeeder.GetVisitors()));

builder.Services.AddSingleton<IRepository<Excursion>>(sp =>
{
    var exhibitionRepo = sp.GetRequiredService<IRepository<Exhibition>>();
    var exhibitions = exhibitionRepo.GetAllAsync().Result.ToList();
    return new InMemoryRepository<Excursion>(MuseumDataSeeder.GetExcursions(exhibitions));
});

builder.Services.AddSingleton<IRepository<Ticket>>(sp =>
{
    var excursionRepo = sp.GetRequiredService<IRepository<Excursion>>();
    var visitorRepo = sp.GetRequiredService<IRepository<Visitor>>();
    var excursions = excursionRepo.GetAllAsync().Result.ToList();
    var visitors = visitorRepo.GetAllAsync().Result.ToList();
    return new InMemoryRepository<Ticket>(MuseumDataSeeder.GetTickets(excursions, visitors));
});

// Регистрация сервисов
builder.Services.AddSingleton<IExhibitionService, ExhibitionService>();
builder.Services.AddSingleton<IExcursionService, ExcursionService>();
builder.Services.AddSingleton<IVisitorService, VisitorService>();
builder.Services.AddSingleton<ITicketService, TicketService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

// Настройка нативного OpenApi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        // Инициализация компонентов
        document.Components ??= new OpenApiComponents();

        // Создаем схему безопасности Bearer
        var bearerScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Введите JWT токен (без слова 'Bearer')"
        };
        document.AddComponent("Bearer", bearerScheme);

        // Создаем требование безопасности через OpenApiSecuritySchemeReference
        var securityRequirement = new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        };

        // Применяем требование ко всем операциям
        foreach (var path in document.Paths.Values)
        {
            // Проверка на null для устранения предупреждения CS8602
            if (path.Operations != null)
            {
                foreach (var operation in path.Operations.Values)
                {
                    operation.Security ??= new List<OpenApiSecurityRequirement>();
                    operation.Security.Add(securityRequirement);
                }
            }
        }

        return Task.CompletedTask;
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("SecretKey не найден в конфигурации");
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();