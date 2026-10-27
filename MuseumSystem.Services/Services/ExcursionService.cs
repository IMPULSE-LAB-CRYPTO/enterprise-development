using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Services.Mappings;

namespace MuseumSystem.Services.Services;

public class ExcursionService : IExcursionService
{
    private readonly List<Excursion> _excursions;
    private readonly List<Exhibition> _exhibitions;
    private readonly object _lock = new();
    private int _nextId;

    public ExcursionService()
    {
        _exhibitions = MuseumDataSeeder.GetExhibitions();
        _excursions = MuseumDataSeeder.GetExcursions(_exhibitions); // ИСПРАВЛЕНО
        _nextId = _excursions.Any() ? _excursions.Max(e => e.Id) + 1 : 1;
    }

    public Task<IEnumerable<ExcursionResponse>> GetAllAsync()
    {
        lock (_lock)
        {
            var result = _excursions.Select(e => e.ToResponse()).ToList();
            return Task.FromResult<IEnumerable<ExcursionResponse>>(result);
        }
    }

    public Task<ExcursionResponse?> GetByIdAsync(int id)
    {
        lock (_lock)
        {
            var entity = _excursions.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(entity?.ToResponse());
        }
    }

    public Task<ExcursionResponse> CreateAsync(ExcursionCreateRequest request)
    {
        lock (_lock)
        {
            var exhibitions = _exhibitions.Where(e => request.ExhibitionIds.Contains(e.Id)).ToList();

            var newEntity = new Excursion
            {
                Id = _nextId++,
                StartsAt = request.StartsAt,
                DurationMinutes = (int)request.Duration.TotalMinutes, // ИСПРАВЛЕНО
                Exhibitions = exhibitions
            };
            _excursions.Add(newEntity);
            return Task.FromResult(newEntity.ToResponse());
        }
    }

    public Task<bool> UpdateAsync(int id, ExcursionUpdateRequest request)
    {
        lock (_lock)
        {
            var entity = _excursions.FirstOrDefault(e => e.Id == id);
            if (entity == null) return Task.FromResult(false);

            entity.StartsAt = request.StartsAt;
            entity.DurationMinutes = (int)request.Duration.TotalMinutes; // ИСПРАВЛЕНО
            entity.Exhibitions = _exhibitions.Where(e => request.ExhibitionIds.Contains(e.Id)).ToList();

            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var entity = _excursions.FirstOrDefault(e => e.Id == id);
            if (entity == null) return Task.FromResult(false);

            _excursions.Remove(entity);
            return Task.FromResult(true);
        }
    }
}