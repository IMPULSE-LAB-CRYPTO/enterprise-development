using System.ComponentModel.DataAnnotations;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Запрос на авторизацию
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// Ответ с JWT токеном
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// JWT токен для доступа к API
    /// </summary>
    public string Token { get; set; } = string.Empty;
}