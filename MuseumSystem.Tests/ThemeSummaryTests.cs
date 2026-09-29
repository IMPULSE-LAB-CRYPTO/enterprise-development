using MuseumSystem.Domain.Enums;

namespace MuseumSystem.Tests;

/// <summary>
/// Тесты сводной информации о посещаемости по тематикам за период
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными</param>
public class ThemeSummaryTests(MuseumFixture fixture) : IClassFixture<MuseumFixture>
{
    /// <summary>
    /// Возвращаем сводку по каждой тематике: общее число посетителей,
    /// общую стоимость билетов, среднее, минимальное и максимальное число посетителей в день
    /// </summary>
    [Fact]
    public void GetThemeSummaryForPeriod_ReturnsAggregatedDataPerTheme()
    {
        // arrange
        var from = new DateOnly(2026, 1, 1);
        var to = new DateOnly(2026, 3, 31);

        var expected = new[]
        {
            new ThemeSummary(ExhibitionTheme.Historical,    7, 3750m, 3.0, 1, 5),
            new ThemeSummary(ExhibitionTheme.Art,          11, 5250m, 4.0, 4, 4),
            new ThemeSummary(ExhibitionTheme.Science,       7, 3750m, 4.5, 3, 6),
            new ThemeSummary(ExhibitionTheme.Ethnographic,  3, 2000m, 2.0, 2, 2),
            new ThemeSummary(ExhibitionTheme.Technical,     6, 3500m, 4.0, 3, 5),
        };

        // act
        var actual = fixture.Tickets
            .Where(t => t.Excursion.Date >= from && t.Excursion.Date <= to)
            .SelectMany(t => t.Excursion.Exhibitions
                .Select(e => e.Theme)
                .Distinct()
                .Select(theme => new { Theme = theme, Ticket = t }))
            .GroupBy(x => x.Theme)
            .Select(g => new ThemeSummary(
                Theme: g.Key,
                TotalVisitors: g.Select(x => x.Ticket.Visitor.Id).Distinct().Count(),
                TotalCost: g.Sum(x => x.Ticket.Price),
                AverageVisitorsPerDay: g.GroupBy(x => x.Ticket.Excursion.Date).Average(d => d.Count()),
                MinVisitorsPerDay: g.GroupBy(x => x.Ticket.Excursion.Date).Min(d => d.Count()),
                MaxVisitorsPerDay: g.GroupBy(x => x.Ticket.Excursion.Date).Max(d => d.Count())))
            .OrderBy(s => s.Theme)
            .ToList();

        // assert
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Сводная информация по одной тематике
    /// </summary>
    private sealed record ThemeSummary(
        ExhibitionTheme Theme,
        int TotalVisitors,
        decimal TotalCost,
        double AverageVisitorsPerDay,
        int MinVisitorsPerDay,
        int MaxVisitorsPerDay);
}