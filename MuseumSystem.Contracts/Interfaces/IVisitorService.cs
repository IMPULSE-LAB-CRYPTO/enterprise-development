using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

/// <summary>
/// Сервис для управления посетителями
/// </summary>
public interface IVisitorService
{
    /// <summary>
    /// Получить список всех посетителей
    /// </summary>
    Task<IEnumerable<VisitorResponse>> GetAllAsync();

    /// <summary>
    /// Получить посетителя по идентификатору
    /// </summary>
    Task<VisitorResponse?> GetByIdAsync(int id);

    /// <summary>
    /// Создать нового посетителя
    /// </summary>
    Task<VisitorResponse> CreateAsync(VisitorCreateRequest request);

    /// <summary>
    /// Обновить данные посетителя
    /// </summary>
    Task<bool> UpdateAsync(int id, VisitorUpdateRequest request);

    /// <summary>
    /// Удалить посетителя
    /// </summary>
    Task<bool> DeleteAsync(int id);
}