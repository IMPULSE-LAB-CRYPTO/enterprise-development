using MuseumSystem.Shared.Enums;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Ответ для топ-5 выставок
/// </summary>
public class TopExhibitionResponse
{
    /// <summary>
    /// Идентификатор выставки
    /// </summary>
    public int ExhibitionId { get; set; }

    /// <summary>
    /// Название выставки
    /// </summary>
    public string ExhibitionName { get; set; } = string.Empty;

    /// <summary>
    /// Количество посетителей
    /// </summary>
    public int VisitorCount { get; set; }
}

/// <summary>
/// Ответ с информацией об экскурсии и количестве участников
/// </summary>
public class ExcursionWithParticipantsResponse
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    public int ExcursionId { get; set; }

    /// <summary>
    /// Время начала экскурсии
    /// </summary>
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Количество участников
    /// </summary>
    public int ParticipantCount { get; set; }
}

/// <summary>
/// Сводная информация о посещаемости по тематике
/// </summary>
public class ThemeSummaryResponse
{
    /// <summary>
    /// Тематика выставки
    /// </summary>
    public ExhibitionTheme Theme { get; set; }

    /// <summary>
    /// Общее количество посетителей
    /// </summary>
    public int TotalVisitors { get; set; }

    /// <summary>
    /// Общая стоимость билетов
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Среднее количество посетителей в день
    /// </summary>
    public double AvgVisitors { get; set; }

    /// <summary>
    /// Минимальное количество посетителей в день
    /// </summary>
    public int MinVisitors { get; set; }

    /// <summary>
    /// Максимальное количество посетителей в день
    /// </summary>
    public int MaxVisitors { get; set; }
}

/// <summary>
/// Информация об экскурсии в заданном зале
/// </summary>
public class ExcursionInHallResponse
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    public int ExcursionId { get; set; }

    /// <summary>
    /// Время начала экскурсии
    /// </summary>
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    public TimeSpan Duration { get; set; }
}