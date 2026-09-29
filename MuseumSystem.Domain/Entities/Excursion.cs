namespace MuseumSystem.Domain.Entities;

/// <summary>
/// Экскурсия в музее
/// </summary>
public class Excursion
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата проведения экскурсии
    /// </summary>
    public required DateOnly Date { get; set; }

    /// <summary>
    /// Время начала экскурсии
    /// </summary>
    public required TimeOnly StartTime { get; set; }

    /// <summary>
    /// Продолжительность экскурсии в минутах
    /// </summary>
    public required int DurationMinutes { get; set; }

    /// <summary>
    /// Номер зала, в котором проводится экскурсия
    /// </summary>
    public required int HallNumber { get; set; }

    /// <summary>
    /// Список посещаемых выставок
    /// </summary>
    public List<Exhibition> Exhibitions { get; set; } = [];
}