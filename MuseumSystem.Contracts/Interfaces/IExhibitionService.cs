using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

/// <summary>
/// Сервис для управления выставками
/// </summary>
public interface IExhibitionService
{
    /// <summary>
    /// Получить список всех выставок
    /// </summary>
    Task<IEnumerable<ExhibitionResponse>> GetAllAsync();

    /// <summary>
    /// Получить выставку по её идентификатору
    /// </summary>
    Task<ExhibitionResponse?> GetByIdAsync(int id);

    /// <summary>
    /// Создать новую выставку
    /// </summary>
    Task<ExhibitionResponse> CreateAsync(ExhibitionCreateRequest request);

    /// <summary>
    /// Обновить данные существующей выставки
    /// </summary>
    Task<bool> UpdateAsync(int id, ExhibitionUpdateRequest request);

    /// <summary>
    /// Удалить выставку
    /// </summary>
    Task<bool> DeleteAsync(int id);
}