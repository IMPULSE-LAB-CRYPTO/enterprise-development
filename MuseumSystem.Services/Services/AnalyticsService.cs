using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Services.Mappings;

namespace MuseumSystem.Services.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly ILogger<AnalyticsService> _logger;
    private readonly List<Ticket> _tickets;
    private readonly List<Excursion> _excursions;

    public AnalyticsService() : this(NullLogger<AnalyticsService>.Instance) { }

    public AnalyticsService(ILogger<AnalyticsService> logger)
    {
        _logger = logger;
        var exhibitions = MuseumDataSeeder.GetExhibitions();
        _excursions = MuseumDataSeeder.GetExcursions(exhibitions);
        _tickets = MuseumDataSeeder.GetTickets(_excursions, MuseumDataSeeder.GetVisitors());
    }

    public Task<IEnumerable<TopExhibitionResponse>> GetTop5ExhibitionsByVisitorsAsync()
    {
        _logger.LogInformation("Запрос топ-5 выставок по посетителям");
        var result = _tickets
            .SelectMany(t => t.Excursion.Exhibitions)
            .GroupBy(e => new { e.Id, e.Name })
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new TopExhibitionResponse
            {
                ExhibitionId = g.Key.Id,
                ExhibitionName = g.Key.Name,
                VisitorCount = g.Count()
            })
            .ToList();

        return Task.FromResult<IEnumerable<TopExhibitionResponse>>(result);
    }

    public Task<IEnumerable<ExcursionWithParticipantsResponse>> GetExcursionsWithMinimalParticipantsAsync()
    {
        _logger.LogInformation("Запрос экскурсий с минимальным числом участников");
        var excursionsWithCount = _tickets
            .GroupBy(t => t.Excursion.Id)
            .Select(g => new { Excursion = g.First().Excursion, Count = g.Count() })
            .ToList();

        if (excursionsWithCount.Count == 0)
            return Task.FromResult<IEnumerable<ExcursionWithParticipantsResponse>>(new List<ExcursionWithParticipantsResponse>());

        var minCount = excursionsWithCount.Min(x => x.Count);

        var result = excursionsWithCount
            .Where(x => x.Count == minCount)
            .Select(x => new ExcursionWithParticipantsResponse
            {
                ExcursionId = x.Excursion.Id,
                StartsAt = x.Excursion.StartsAt,
                ParticipantCount = x.Count
            })
            .ToList();

        return Task.FromResult<IEnumerable<ExcursionWithParticipantsResponse>>(result);
    }

    public Task<IEnumerable<ThemeSummaryResponse>> GetThemeSummaryForPeriodAsync(DateTime from, DateTime to)
    {
        _logger.LogInformation("Запрос сводки по тематикам за период с {From} по {To}", from, to);
        var result = _tickets
            .Where(t => t.Excursion.StartsAt >= from && t.Excursion.StartsAt <= to)
            .SelectMany(t => t.Excursion.Exhibitions
                .Select(e => e.Theme)
                .Distinct()
                .Select(theme => new { Theme = theme, Ticket = t }))
            .GroupBy(x => x.Theme)
            .Select(g => new ThemeSummaryResponse
            {
                Theme = g.Key,
                TotalVisitors = g.Select(x => x.Ticket.Visitor.Id).Distinct().Count(),
                TotalCost = g.Sum(x => x.Ticket.Price),
                AvgVisitors = g.GroupBy(x => DateOnly.FromDateTime(x.Ticket.Excursion.StartsAt)).Average(d => d.Count()),
                MinVisitors = g.GroupBy(x => DateOnly.FromDateTime(x.Ticket.Excursion.StartsAt)).Min(d => d.Count()),
                MaxVisitors = g.GroupBy(x => DateOnly.FromDateTime(x.Ticket.Excursion.StartsAt)).Max(d => d.Count())
            })
            .OrderBy(s => s.Theme)
            .ToList();

        return Task.FromResult<IEnumerable<ThemeSummaryResponse>>(result);
    }

    public Task<IEnumerable<ExcursionInHallResponse>> GetExcursionsInHallForPeriodAsync(int hallNumber, DateTime from, DateTime to)
    {
        _logger.LogInformation("Запрос экскурсий в зале {HallNumber} за период", hallNumber);
        var result = _excursions
            .Where(e => e.Exhibitions.Any(ex => ex.HallNumber == hallNumber)
                        && e.StartsAt >= from
                        && e.StartsAt <= to)
            .Select(e => new ExcursionInHallResponse
            {
                ExcursionId = e.Id,
                StartsAt = e.StartsAt,
                Duration = TimeSpan.FromMinutes(e.DurationMinutes)
            })
            .OrderBy(x => x.ExcursionId)
            .ToList();

        return Task.FromResult<IEnumerable<ExcursionInHallResponse>>(result);
    }

    public Task<IEnumerable<VisitorResponse>> GetVisitorsOfSelectedExcursionAsync(int excursionId)
    {
        _logger.LogInformation("Запрос посетителей экскурсии {ExcursionId}", excursionId);
        var result = _tickets
            .Where(t => t.Excursion.Id == excursionId)
            .Select(t => t.Visitor.ToResponse())
            .OrderBy(v => v.FullName)
            .ToList();

        return Task.FromResult<IEnumerable<VisitorResponse>>(result);
    }
}