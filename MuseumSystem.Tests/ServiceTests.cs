using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Enums;
using MuseumSystem.Services.Services;

namespace MuseumSystem.Tests;

public class ServiceTests
{
    // Тесты для ExhibitionService

    [Fact]
    public async Task ExhibitionService_GetAll_ReturnsSeededData()
    {
        // Arrange
        var service = new ExhibitionService();

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() >= 10); // В сидере минимум 10 записей
    }

    [Fact]
    public async Task ExhibitionService_CreateAndDelete_WorksCorrectly()
    {
        // Arrange
        var service = new ExhibitionService();
        var newExhibition = new ExhibitionCreateRequest
        {
            Name = "Тестовая выставка",
            Theme = ExhibitionTheme.Science,
            HallNumber = 5,
            StartDate = new DateOnly(2026, 5, 1),
            EndDate = new DateOnly(2026, 6, 1)
        };

        // Act
        var created = await service.CreateAsync(newExhibition);
        var allAfterCreate = await service.GetAllAsync();

        var isDeleted = await service.DeleteAsync(created.Id);
        var allAfterDelete = await service.GetAllAsync();

        // Assert
        Assert.NotNull(created);
        Assert.True(created.Id > 0);
        Assert.Contains(allAfterCreate, e => e.Id == created.Id);

        Assert.True(isDeleted);
        Assert.DoesNotContain(allAfterDelete, e => e.Id == created.Id);
    }

    // Тесты для AnalyticsService

    [Fact]
    public async Task AnalyticsService_GetTop5Exhibitions_ReturnsFiveItems()
    {
        // Arrange
        var service = new AnalyticsService();

        // Act
        var result = await service.GetTop5ExhibitionsByVisitorsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count());
        // Проверяем, что первая выставка имеет наибольшее количество посетителей
        Assert.True(result.First().VisitorCount >= result.Last().VisitorCount);
    }

    [Fact]
    public async Task AnalyticsService_GetThemeSummary_ReturnsDataForPeriod()
    {
        // Arrange
        var service = new AnalyticsService();
        var from = new DateTime(2026, 1, 1);
        var to = new DateTime(2026, 12, 31);

        // Act
        var result = await service.GetThemeSummaryForPeriodAsync(from, to);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        // Проверяем, что все темы уникальны (сгруппированы)
        Assert.Equal(result.Count(), result.Select(x => x.Theme).Distinct().Count());
    }

    [Fact]
    public async Task AnalyticsService_GetVisitorsOfSelectedExcursion_ReturnsSortedByName()
    {
        // Arrange
        var service = new AnalyticsService();
        var excursionId = 3; // Берем ID из сидера

        // Act
        var result = await service.GetVisitorsOfSelectedExcursionAsync(excursionId);

        // Assert
        Assert.NotNull(result);
        if (result.Count() > 1)
        {
            var names = result.Select(x => x.FullName).ToList();
            var sortedNames = names.OrderBy(n => n).ToList();
            Assert.Equal(sortedNames, names); // Проверка сортировки
        }
    }
}