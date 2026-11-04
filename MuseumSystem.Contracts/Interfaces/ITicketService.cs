using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

/// <summary>
/// Сервис для управления билетами
/// </summary>
public interface ITicketService
{
    /// <summary>
    /// Получить список всех билетов
    /// </summary>
    Task<IEnumerable<TicketResponse>> GetAllAsync();

    /// <summary>
    /// Получить билет по идентификатору
    /// </summary>
    Task<TicketResponse?> GetByIdAsync(int id);

    /// <summary>
    /// Создать новый билет
    /// </summary>
    Task<TicketResponse> CreateAsync(TicketCreateRequest request);

    /// <summary>
    /// Обновить данные билета
    /// </summary>
    Task<bool> UpdateAsync(int id, TicketUpdateRequest request);

    /// <summary>
    /// Удалить билет
    /// </summary>
    Task<bool> DeleteAsync(int id);
}