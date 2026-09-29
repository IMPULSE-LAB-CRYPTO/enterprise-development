namespace MuseumSystem.Tests;

/// <summary>
/// Тесты для поиска экскурсий в выбранном зале за указанный период
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными</param>
public class HallExcursionsTests(MuseumFixture fixture) : IClassFixture<MuseumFixture>
{
    /// <summary>
    /// Возвращаем экскурсии только в выбранном зале и только за указанный период
    /// </summary>
    [Fact]
    public void GetExcursionsInSelectedHallForPeriod_ReturnsFilteredExcursions()
    {
        // arrange
        const int hallNumber = 1;
        var from = new DateTime(2026, 1, 1);
        var to = new DateTime(2026, 3, 31, 23, 59, 59);
        int[] expectedIds = [1, 6, 11];

        // act
        var actualIds = fixture.Excursions
            .Where(e => e.Exhibitions.Any(ex => ex.HallNumber == hallNumber)
                        && e.StartsAt >= from
                        && e.StartsAt <= to)
            .Select(e => e.Id)
            .OrderBy(id => id)
            .ToList();

        // assert
        Assert.Equal(expectedIds, actualIds);
    }
}