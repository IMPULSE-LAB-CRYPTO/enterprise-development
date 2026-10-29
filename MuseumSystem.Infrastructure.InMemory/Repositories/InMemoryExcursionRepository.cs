using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;

namespace MuseumSystem.Infrastructure.InMemory.Repositories;

public class InMemoryExcursionRepository : IExcursionRepository
{
    private readonly List<Excursion> _excursions;
    private readonly IExhibitionRepository _exhibitionRepository;
    private readonly object _lock = new();
    private int _nextId;

    public InMemoryExcursionRepository(IExhibitionRepository exhibitionRepository)
    {
        _exhibitionRepository = exhibitionRepository;
        var exhibitions = _exhibitionRepository.GetAllAsync().Result.ToList();
        _excursions = MuseumDataSeeder.GetExcursions(exhibitions);
        _nextId = _excursions.Count > 0 ? _excursions.Max(e => e.Id) + 1 : 1;
    }

    public Task<IEnumerable<Excursion>> GetAllAsync()
    {
        lock (_lock) return Task.FromResult<IEnumerable<Excursion>>(_excursions.ToList());
    }

    public Task<Excursion?> GetByIdAsync(int id)
    {
        lock (_lock) return Task.FromResult(_excursions.FirstOrDefault(e => e.Id == id));
    }

    public Task<Excursion> AddAsync(Excursion excursion)
    {
        lock (_lock)
        {
            excursion.Id = _nextId++;
            _excursions.Add(excursion);
            return Task.FromResult(excursion);
        }
    }

    public Task<Excursion?> UpdateAsync(Excursion excursion)
    {
        lock (_lock)
        {
            var existing = _excursions.FirstOrDefault(e => e.Id == excursion.Id);
            if (existing == null) return Task.FromResult<Excursion?>(null);

            existing.StartsAt = excursion.StartsAt;
            existing.DurationMinutes = excursion.DurationMinutes;
            existing.Exhibitions = excursion.Exhibitions;

            return Task.FromResult<Excursion?>(existing);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var existing = _excursions.FirstOrDefault(e => e.Id == id);
            if (existing == null) return Task.FromResult(false);

            _excursions.Remove(existing);
            return Task.FromResult(true);
        }
    }
}