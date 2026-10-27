using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

/// <summary>
/// Управление экскурсиями
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExcursionsController(IExcursionService service) : ControllerBase
{
    /// <summary>
    /// Получить список всех экскурсий
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExcursionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExcursionResponse>>> GetAll()
        => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получить экскурсию по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExcursionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExcursionResponse>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Создать новую экскурсию
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ExcursionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExcursionResponse>> Create([FromBody] ExcursionCreateRequest request)
    {
        var result = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Обновить экскурсию
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(int id, [FromBody] ExcursionUpdateRequest request)
    {
        var success = await service.UpdateAsync(id, request);
        return success ? NoContent() : NotFound();
    }

    /// <summary>
    /// Удалить экскурсию
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