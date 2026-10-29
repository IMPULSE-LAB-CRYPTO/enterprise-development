using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Domain.Interfaces;

public interface IExhibitionRepository
{
    Task<IEnumerable<Exhibition>> GetAllAsync();
    Task<Exhibition?> GetByIdAsync(int id);
    Task<Exhibition> AddAsync(Exhibition exhibition);
    Task<Exhibition?> UpdateAsync(Exhibition exhibition);
    Task<bool> DeleteAsync(int id);
}