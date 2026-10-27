using System.ComponentModel.DataAnnotations;
using MuseumSystem.Domain.Enums;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные выставки для ответа клиенту
/// </summary>
public class ExhibitionResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ExhibitionTheme Theme { get; set; }
    public int HallNumber { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}

/// <summary>
/// Данные для создания новой выставки
/// </summary>
public class ExhibitionCreateRequest
{
    [Required(ErrorMessage = "Название обязательно")]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public ExhibitionTheme Theme { get; set; }

    [Range(1, 100)]
    public int HallNumber { get; set; }

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }
}

/// <summary>
/// Данные для обновления выставки
/// </summary>
public class ExhibitionUpdateRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public ExhibitionTheme Theme { get; set; }

    [Range(1, 100)]
    public int HallNumber { get; set; }

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }
}