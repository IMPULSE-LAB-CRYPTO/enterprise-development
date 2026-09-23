using MuseumSystem.Domain.Enums;

namespace MuseumSystem.Domain.Entities;

/// <summary>
/// выставка музея
/// </summary>
public class Exhibition
{
    /// <summary>
    /// идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// название выставки
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// тематика выставки
    /// </summary>
    public required ExhibitionTheme Theme { get; set; }

    /// <summary>
    /// номер зала, в котором проходит выставка
    /// </summary>
    public required int HallNumber { get; set; }

    /// <summary>
    /// дата начала выставки
    /// </summary>
    public required DateOnly StartDate { get; set; }

    /// <summary>
    /// дата окончания выставки
    /// </summary>
    public required DateOnly EndDate { get; set; }
}