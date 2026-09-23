using MuseumSystem.Domain.Enums;

namespace MuseumSystem.Domain.Entities;

/// <summary>
/// билет на экскурсию (в качестве контракта)
/// </summary>
public class Ticket
{
    /// <summary>
    /// идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// экскурсия, на которую куплен билет
    /// </summary>
    public required Excursion Excursion { get; set; }

    /// <summary>
    /// посетитель, купивший билет
    /// </summary>
    public required Visitor Visitor { get; set; }

    /// <summary>
    /// тип билета
    /// </summary>
    public required TicketType Type { get; set; }

    /// <summary>
    /// цена билета
    /// </summary>
    public required decimal Price { get; set; }
}