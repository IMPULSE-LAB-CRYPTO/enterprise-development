using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Domain.Interfaces;

public interface IExcursionRepository
{
    Task<IEnumerable<Excursion>> GetAllAsync();
    Task<Excursion?> GetByIdAsync(int id);
    Task<Excursion> AddAsync(Excursion excursion);
    Task<Excursion?> UpdateAsync(Excursion excursion);
    Task<bool> DeleteAsync(int id);
}