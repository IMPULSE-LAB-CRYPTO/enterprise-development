using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;

namespace MuseumSystem.Infrastructure.InMemory.Repositories;

public class InMemoryExhibitionRepository : IExhibitionRepository
{
    private readonly List<Exhibition> _exhibitions;
    private readonly object _lock = new();
    private int _nextId;

    public InMemoryExhibitionRepository()
    {
        _exhibitions = MuseumDataSeeder.GetExhibitions();
        _nextId = _exhibitions.Count > 0 ? _exhibitions.Max(e => e.Id) + 1 : 1;
    }

    public Task<IEnumerable<Exhibition>> GetAllAsync()
    {
        lock (_lock) return Task.FromResult<IEnumerable<Exhibition>>(_exhibitions.ToList());
    }

    public Task<Exhibition?> GetByIdAsync(int id)
    {
        lock (_lock) return Task.FromResult(_exhibitions.FirstOrDefault(e => e.Id == id));
    }

    public Task<Exhibition> AddAsync(Exhibition exhibition)
    {
        lock (_lock)
        {
            exhibition.Id = _nextId++;
            _exhibitions.Add(exhibition);
            return Task.FromResult(exhibition);
        }
    }

    public Task<Exhibition?> UpdateAsync(Exhibition exhibition)
    {
        lock (_lock)
        {
            var existing = _exhibitions.FirstOrDefault(e => e.Id == exhibition.Id);
            if (existing == null) return Task.FromResult<Exhibition?>(null);

            // Обновляем свойства существующей сущности в памяти
            existing.Name = exhibition.Name;
            existing.Theme = exhibition.Theme;
            existing.HallNumber = exhibition.HallNumber;
            existing.StartDate = exhibition.StartDate;
            existing.EndDate = exhibition.EndDate;

            return Task.FromResult<Exhibition?>(existing);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var existing = _exhibitions.FirstOrDefault(e => e.Id == id);
            if (existing == null) return Task.FromResult(false);

            _exhibitions.Remove(existing);
            return Task.FromResult(true);
        }
    }
}