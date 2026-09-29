namespace MuseumSystem.Tests;

/// <summary>
/// Тесты для определения топ-5 выставок по количеству посетителей
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными.</param>
public class TopExhibitionsTests(MuseumFixture fixture) : IClassFixture<MuseumFixture>
{
    /// <summary>
    /// Возвращает топ-5 выставок, упорядоченных п убыванию числа посетителей
    /// </summary>
    [Fact]
    public void GetTop5ExhibitionsByVisitors_ReturnsOrderedByVisitorCount()
    {
        // arrange
        var expectedTop5 = new[]
         {
            "Физика частиц",
            "Древний Египет",
            "Импрессионизм",
            "Авиация",
            "Космос",
        };

        // act
        var actualTop5 = fixture.Tickets
            .SelectMany(t => t.Excursion.Exhibitions)
            .GroupBy(e => e.Name)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        // assert
        Assert.Equal(expectedTop5, actualTop5);
    }
}