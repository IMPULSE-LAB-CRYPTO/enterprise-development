using System.ComponentModel.DataAnnotations;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные экскурсии для ответа
/// </summary>
public class ExcursionResponse
{
    /// <summary>
    /// Уникальный идентификатор экскурсии
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время начала экскурсии
    /// </summary>
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Список ID выставок, которые посещаются
    /// </summary>
    public List<int> ExhibitionIds { get; set; } = new();
}

/// <summary>
/// Данные для создания экскурсии
/// </summary>
public class ExcursionCreateRequest
{
    /// <summary>
    /// Дата и время начала экскурсии
    /// </summary>
    [Required]
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    [Required]
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Список ID выставок, которые посещаются
    /// </summary>
    public List<int> ExhibitionIds { get; set; } = new();
}

/// <summary>
/// Данные для обновления экскурсии
/// </summary>
public class ExcursionUpdateRequest
{
    /// <summary>
    /// Дата и время начала экскурсии
    /// </summary>
    [Required]
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    [Required]
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Список ID выставок, которые посещаются
    /// </summary>
    public List<int> ExhibitionIds { get; set; } = new();
}