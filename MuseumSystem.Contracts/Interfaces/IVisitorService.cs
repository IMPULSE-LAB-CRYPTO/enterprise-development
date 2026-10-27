using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

public interface IVisitorService
{
    Task<IEnumerable<VisitorResponse>> GetAllAsync();
    Task<VisitorResponse?> GetByIdAsync(int id);
    Task<VisitorResponse> CreateAsync(VisitorCreateRequest request);
    Task<bool> UpdateAsync(int id, VisitorUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}