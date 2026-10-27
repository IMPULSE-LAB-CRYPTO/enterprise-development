using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSystem.Contracts.Interfaces;

namespace MuseumSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _service;

    public AnalyticsController(IAnalyticsService service) => _service = service;

    [HttpGet("top-exhibitions")]
    public async Task<IActionResult> GetTop5Exhibitions() => Ok(await _service.GetTop5ExhibitionsByVisitorsAsync());

    [HttpGet("minimal-participants")]
    public async Task<IActionResult> GetMinimalParticipants() => Ok(await _service.GetExcursionsWithMinimalParticipantsAsync());

    [HttpGet("theme-summary")]
    public async Task<IActionResult> GetThemeSummary([FromQuery] DateTime from, [FromQuery] DateTime to)
        => Ok(await _service.GetThemeSummaryForPeriodAsync(from, to));

    [HttpGet("excursions-in-hall")]
    public async Task<IActionResult> GetExcursionsInHall([FromQuery] int hallNumber, [FromQuery] DateTime from, [FromQuery] DateTime to)
        => Ok(await _service.GetExcursionsInHallForPeriodAsync(hallNumber, from, to));

    [HttpGet("visitors-of-excursion/{excursionId}")]
    public async Task<IActionResult> GetVisitorsOfExcursion(int excursionId)
        => Ok(await _service.GetVisitorsOfSelectedExcursionAsync(excursionId));
}