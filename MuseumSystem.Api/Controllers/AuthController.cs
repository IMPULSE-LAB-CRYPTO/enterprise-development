using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Для теста
        if (request.Username != "admin" || request.Password != "password")
            return Unauthorized("Неверный логин или пароль");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("SuperSecretKeyForMuseumSystemLab2_1234567890!");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, request.Username) }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "MuseumSystem",
            Audience = "MuseumSystemUsers",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new LoginResponse { Token = tokenHandler.WriteToken(token) });
    }
}