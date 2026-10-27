using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

/// <summary>
/// Управление посетителями
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VisitorsController(IVisitorService service) : ControllerBase
{
    /// <summary>
    /// Получить список всех посетителей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VisitorResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VisitorResponse>>> GetAll()
        => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получить посетителя по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VisitorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VisitorResponse>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Создать нового посетителя
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(VisitorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VisitorResponse>> Create([FromBody] VisitorCreateRequest request)
    {
        var result = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Обновить данные посетителя
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(int id, [FromBody] VisitorUpdateRequest request)
    {
        var success = await service.UpdateAsync(id, request);
        return success ? NoContent() : NotFound();
    }

    /// <summary>
    /// Удалить посетителя
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}