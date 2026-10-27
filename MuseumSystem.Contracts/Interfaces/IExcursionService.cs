using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

public interface IExcursionService
{
    Task<IEnumerable<ExcursionResponse>> GetAllAsync();
    Task<ExcursionResponse?> GetByIdAsync(int id);
    Task<ExcursionResponse> CreateAsync(ExcursionCreateRequest request);
    Task<bool> UpdateAsync(int id, ExcursionUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}