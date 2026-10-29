using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Domain.Interfaces;
using MuseumSystem.Infrastructure.InMemory.Repositories;
using MuseumSystem.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Регистрация репозиториев (Singleton для сохранения данных в памяти)
builder.Services.AddSingleton<IExhibitionRepository, InMemoryExhibitionRepository>();
builder.Services.AddSingleton<IVisitorRepository, InMemoryVisitorRepository>();
builder.Services.AddSingleton<IExcursionRepository, InMemoryExcursionRepository>();
builder.Services.AddSingleton<ITicketRepository, InMemoryTicketRepository>();

// Регистрация сервисов
builder.Services.AddSingleton<IExhibitionService, ExhibitionService>();
builder.Services.AddSingleton<IExcursionService, ExcursionService>();
builder.Services.AddSingleton<IVisitorService, VisitorService>();
builder.Services.AddSingleton<ITicketService, TicketService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Museum API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Введите JWT токен (без слова 'Bearer')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();