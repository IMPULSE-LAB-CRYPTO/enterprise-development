using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;

namespace MuseumSystem.Infrastructure.InMemory.Repositories;

public class InMemoryVisitorRepository : IVisitorRepository
{
    private readonly List<Visitor> _visitors;
    private readonly object _lock = new();
    private int _nextId;

    public InMemoryVisitorRepository()
    {
        _visitors = MuseumDataSeeder.GetVisitors();
        _nextId = _visitors.Count > 0 ? _visitors.Max(v => v.Id) + 1 : 1;
    }

    public Task<IEnumerable<Visitor>> GetAllAsync()
    {
        lock (_lock) return Task.FromResult<IEnumerable<Visitor>>(_visitors.ToList());
    }

    public Task<Visitor?> GetByIdAsync(int id)
    {
        lock (_lock) return Task.FromResult(_visitors.FirstOrDefault(v => v.Id == id));
    }

    public Task<Visitor> AddAsync(Visitor visitor)
    {
        lock (_lock)
        {
            visitor.Id = _nextId++;
            _visitors.Add(visitor);
            return Task.FromResult(visitor);
        }
    }

    public Task<Visitor?> UpdateAsync(Visitor visitor)
    {
        lock (_lock)
        {
            var existing = _visitors.FirstOrDefault(v => v.Id == visitor.Id);
            if (existing == null) return Task.FromResult<Visitor?>(null);

            existing.FullName = visitor.FullName;
            existing.Phone = visitor.Phone;
            existing.BirthDate = visitor.BirthDate;

            return Task.FromResult<Visitor?>(existing);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var existing = _visitors.FirstOrDefault(v => v.Id == id);
            if (existing == null) return Task.FromResult(false);

            _visitors.Remove(existing);
            return Task.FromResult(true);
        }
    }
}