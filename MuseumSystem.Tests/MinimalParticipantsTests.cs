namespace MuseumSystem.Tests;

/// <summary>
/// Тесты для поиска экскурсий с минимальным количеством участников
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными</param>
public class MinimalParticipantsTests(MuseumFixture fixture) : IClassFixture<MuseumFixture>
{
    /// <summary>
    /// Возвращаем экскурсии с минимальным числом участников
    /// </summary>
    [Fact]
    public void GetExcursionsWithMinimalParticipants_ReturnsExcursionsWithMinCount()
    {
        // arrange
        var excursionsWithCount = fixture.Tickets
            .GroupBy(t => t.Excursion.Id)
            .Select(g => new { ExcursionId = g.Key, Count = g.Count() })
            .ToList();

        var minCount = excursionsWithCount.Min(x => x.Count);

        // act
        var actualExcursionIds = excursionsWithCount
            .Where(x => x.Count == minCount)
            .Select(x => x.ExcursionId)
            .OrderBy(id => id)
            .ToList();

        // assert
        Assert.Equal(1, minCount);
        Assert.Equal(new[] { 6 }, actualExcursionIds);
    }
}