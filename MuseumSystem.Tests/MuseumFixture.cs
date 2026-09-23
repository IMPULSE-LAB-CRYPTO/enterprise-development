using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Tests;

/// <summary>
/// фикстура с тестовыми данными музея (один раз на класс тестов)
/// </summary>
public class MuseumFixture
{
    /// <summary>
    /// список выставок
    /// </summary>
    public List<Exhibition> Exhibitions { get; }

    /// <summary>
    /// список посетителей
    /// </summary>
    public List<Visitor> Visitors { get; }

    /// <summary>
    /// список экскурсий
    /// </summary>
    public List<Excursion> Excursions { get; }

    /// <summary>
    /// список билетов
    /// </summary>
    public List<Ticket> Tickets { get; }

    /// <summary>
    /// инициализирует фикстуру тестовыми данными
    /// </summary>
    public MuseumFixture()
    {
        Exhibitions = MuseumDataSeeder.GetExhibitions();
        Visitors = MuseumDataSeeder.GetVisitors();
        Excursions = MuseumDataSeeder.GetExcursions(Exhibitions);
        Tickets = MuseumDataSeeder.GetTickets(Excursions, Visitors);
    }
}
