using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

/// <summary>
/// Аналитические запросы по данным музея
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnalyticsController(IAnalyticsService service) : ControllerBase
{
    /// <summary>
    /// Топ-5 выставок по количеству посетителей
    /// </summary>
    [HttpGet("top-exhibitions")]
    [ProducesResponseType(typeof(IEnumerable<TopExhibitionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TopExhibitionResponse>>> GetTop5Exhibitions()
        => Ok(await service.GetTop5ExhibitionsByVisitorsAsync());

    /// <summary>
    /// Экскурсии с минимальным числом участников
    /// </summary>
    [HttpGet("minimal-participants")]
    [ProducesResponseType(typeof(IEnumerable<ExcursionWithParticipantsResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExcursionWithParticipantsResponse>>> GetMinimalParticipants()
        => Ok(await service.GetExcursionsWithMinimalParticipantsAsync());

    /// <summary>
    /// Сводная информация по тематикам за период
    /// </summary>
    [HttpGet("theme-summary")]
    [ProducesResponseType(typeof(IEnumerable<ThemeSummaryResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ThemeSummaryResponse>>> GetThemeSummary(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
        => Ok(await service.GetThemeSummaryForPeriodAsync(from, to));

    /// <summary>
    /// Экскурсии в выбранном зале за период
    /// </summary>
    [HttpGet("excursions-in-hall")]
    [ProducesResponseType(typeof(IEnumerable<ExcursionInHallResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExcursionInHallResponse>>> GetExcursionsInHall(
        [FromQuery] int hallNumber,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
        => Ok(await service.GetExcursionsInHallForPeriodAsync(hallNumber, from, to));

    /// <summary>
    /// Посетители выбранной экскурсии
    /// </summary>
    [HttpGet("visitors-of-excursion/{excursionId}")]
    [ProducesResponseType(typeof(IEnumerable<VisitorResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VisitorResponse>>> GetVisitorsOfExcursion(int excursionId)
        => Ok(await service.GetVisitorsOfSelectedExcursionAsync(excursionId));
}