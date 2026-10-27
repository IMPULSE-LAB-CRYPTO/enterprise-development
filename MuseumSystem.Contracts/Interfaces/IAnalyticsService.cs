using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

public interface IAnalyticsService
{
    Task<IEnumerable<TopExhibitionResponse>> GetTop5ExhibitionsByVisitorsAsync();
    Task<IEnumerable<ExcursionWithParticipantsResponse>> GetExcursionsWithMinimalParticipantsAsync();
    Task<IEnumerable<ThemeSummaryResponse>> GetThemeSummaryForPeriodAsync(DateTime from, DateTime to);
    Task<IEnumerable<ExcursionInHallResponse>> GetExcursionsInHallForPeriodAsync(int hallNumber, DateTime from, DateTime to);
    Task<IEnumerable<VisitorResponse>> GetVisitorsOfSelectedExcursionAsync(int excursionId);
}