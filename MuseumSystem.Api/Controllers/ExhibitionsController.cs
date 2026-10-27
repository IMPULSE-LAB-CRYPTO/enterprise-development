using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

/// <summary>
/// Управление выставками музея
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize] // эндпоинты вместе сJWT-токеном
public class ExhibitionsController(IExhibitionService service) : ControllerBase
{
    /// <summary>
    /// Получить список всех выставок
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExhibitionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExhibitionResponse>>> GetAll()
        => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получить выставку по её идентификатору
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExhibitionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExhibitionResponse>> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Создать новую выставку
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ExhibitionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExhibitionResponse>> Create([FromBody] ExhibitionCreateRequest request)
    {
        var result = await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Обновить данные существующей выставки
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(int id, [FromBody] ExhibitionUpdateRequest request)
    {
        var success = await service.UpdateAsync(id, request);
        return success ? NoContent() : NotFound();
    }

    /// <summary>
    /// Удалить выставку
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