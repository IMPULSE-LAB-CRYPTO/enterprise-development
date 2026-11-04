using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Domain.Data; // Для сидера
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;
using MuseumSystem.Infrastructure.InMemory.Repositories;
using MuseumSystem.Services.Services;
using System.Reflection;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Museum API", Version = "v1" });

    // Читаем XML-файлы для документации
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    var contractsXmlFile = "MuseumSystem.Contracts.xml";
    var contractsXmlPath = Path.Combine(AppContext.BaseDirectory, contractsXmlFile);
    if (File.Exists(contractsXmlPath))
    {
        c.IncludeXmlComments(contractsXmlPath);
    }

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