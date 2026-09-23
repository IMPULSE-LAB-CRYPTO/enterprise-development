namespace MuseumSystem.Tests;

/// <summary>
/// тесты для поиска посетителей выбранной экскурсии
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными</param>
public class ExcursionVisitorsTests(MuseumFixture fixture) : IClassFixture<MuseumFixture>
{
    /// <summary>
    /// Возвращаем посетителей выбранной экскурсии, упорядоченных по ФИО
    /// </summary>
    [Fact]
    public void GetVisitorsOfSelectedExcursion_ReturnsOrderedByFullName()
    {
        // arrange
        const int excursionId = 3;
        var expectedNames = new[]
        {
            "Андреев Алексей Сергеевич",
            "Борисова Марина Владимировна",
            "Васильев Дмитрий Олегович",
            "Козлова Мария Дмитриевна",
            "Лебедев Артём Игоревич",
            "Морозова Татьяна Львовна",
        };

        // act
        var actualNames = fixture.Tickets
            .Where(t => t.Excursion.Id == excursionId)
            .Select(t => t.Visitor.FullName)
            .OrderBy(name => name)
            .ToList();

        // assert
        Assert.Equal(expectedNames, actualNames);
    }
}