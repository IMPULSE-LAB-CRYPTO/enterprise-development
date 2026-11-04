using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

/// <summary>
/// Сервис для управления экскурсиями
/// </summary>
public interface IExcursionService
{
    /// <summary>
    /// Получить список всех экскурсий
    /// </summary>
    Task<IEnumerable<ExcursionResponse>> GetAllAsync();

    /// <summary>
    /// Получить экскурсию по идентификатору
    /// </summary>
    Task<ExcursionResponse?> GetByIdAsync(int id);

    /// <summary>
    /// Создать новую экскурсию
    /// </summary>
    Task<ExcursionResponse> CreateAsync(ExcursionCreateRequest request);

    /// <summary>
    /// Обновить экскурсию
    /// </summary>
    Task<bool> UpdateAsync(int id, ExcursionUpdateRequest request);

    /// <summary>
    /// Удалить экскурсию
    /// </summary>
    Task<bool> DeleteAsync(int id);
}