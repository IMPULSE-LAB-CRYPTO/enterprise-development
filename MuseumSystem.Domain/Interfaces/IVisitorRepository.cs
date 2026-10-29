using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Domain.Interfaces;

public interface IVisitorRepository
{
    Task<IEnumerable<Visitor>> GetAllAsync();
    Task<Visitor?> GetByIdAsync(int id);
    Task<Visitor> AddAsync(Visitor visitor);
    Task<Visitor?> UpdateAsync(Visitor visitor);
    Task<bool> DeleteAsync(int id);
}