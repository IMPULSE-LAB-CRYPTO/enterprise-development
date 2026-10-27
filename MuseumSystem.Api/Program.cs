using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IExhibitionService, ExhibitionService>();
builder.Services.AddSingleton<IExcursionService, ExcursionService>();
builder.Services.AddSingleton<IVisitorService, VisitorService>();
builder.Services.AddSingleton<ITicketService, TicketService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Museum API", Version = "v1" });

    // 1. Регистрируем схему безопасности "Bearer"
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Введите JWT токен (без слова 'Bearer')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http, // Важно: Http, а не ApiKey
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // 2. Применяем схему ко всем эндпоинтам (новый синтаксис для .NET 10)
    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

var jwtKey = "SuperSecretKeyForMuseumSystemLab2_1234567890!";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "MuseumSystem",
            ValidAudience = "MuseumSystemUsers",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); //чтобы не было ошибки порта
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();