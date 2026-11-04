using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

/// <summary>
/// Сервис аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить топ-5 выставок по количеству посетителей
    /// </summary>
    Task<IEnumerable<TopExhibitionResponse>> GetTop5ExhibitionsByVisitorsAsync();

    /// <summary>
    /// Получить экскурсии с минимальным числом участников
    /// </summary>
    Task<IEnumerable<ExcursionWithParticipantsResponse>> GetExcursionsWithMinimalParticipantsAsync();

    /// <summary>
    /// Получить сводную информацию по тематикам за период
    /// </summary>
    Task<IEnumerable<ThemeSummaryResponse>> GetThemeSummaryForPeriodAsync(DateTime from, DateTime to);

    /// <summary>
    /// Получить экскурсии в выбранном зале за период
    /// </summary>
    Task<IEnumerable<ExcursionInHallResponse>> GetExcursionsInHallForPeriodAsync(int hallNumber, DateTime from, DateTime to);

    /// <summary>
    /// Получить посетителей выбранной экскурсии
    /// </summary>
    Task<IEnumerable<VisitorResponse>> GetVisitorsOfSelectedExcursionAsync(int excursionId);
}