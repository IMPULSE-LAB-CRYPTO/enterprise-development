namespace MuseumSystem.Domain.Entities;

/// <summary>
/// экскурсия в музее
/// </summary>
public class Excursion
{
    /// <summary>
    /// идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// дата проведения экскурсии
    /// </summary>
    public required DateOnly Date { get; set; }

    /// <summary>
    /// время начала экскурсии
    /// </summary>
    public required TimeOnly StartTime { get; set; }

    /// <summary>
    /// продолжительность экскурсии в минутах
    /// </summary>
    public required int DurationMinutes { get; set; }

    /// <summary>
    /// номер зала, в котором проводится экскурсия
    /// </summary>
    public required int HallNumber { get; set; }

    /// <summary>
    /// список посещаемых выставок
    /// </summary>
    public List<Exhibition> Exhibitions { get; set; } = [];
}