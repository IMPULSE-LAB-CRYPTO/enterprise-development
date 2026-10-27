using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

public interface ITicketService
{
    Task<IEnumerable<TicketResponse>> GetAllAsync();
    Task<TicketResponse?> GetByIdAsync(int id);
    Task<TicketResponse> CreateAsync(TicketCreateRequest request);
    Task<bool> UpdateAsync(int id, TicketUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}