using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

public interface IExhibitionService
{
    Task<IEnumerable<ExhibitionResponse>> GetAllAsync();
    Task<ExhibitionResponse?> GetByIdAsync(int id);
    Task<ExhibitionResponse> CreateAsync(ExhibitionCreateRequest request);
    Task<bool> UpdateAsync(int id, ExhibitionUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}