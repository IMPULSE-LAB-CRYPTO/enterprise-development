using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Services.Mappings;

namespace MuseumSystem.Services.Services;

public class ExhibitionService : IExhibitionService
{
    private readonly List<Exhibition> _exhibitions;
    private readonly object _lock = new();
    private int _nextId;

    public ExhibitionService()
    {
        _exhibitions = MuseumDataSeeder.GetExhibitions();
        _nextId = _exhibitions.Any() ? _exhibitions.Max(e => e.Id) + 1 : 1;
    }

    public Task<IEnumerable<ExhibitionResponse>> GetAllAsync()
    {
        lock (_lock)
        {
            var result = _exhibitions.Select(e => e.ToResponse()).ToList();
            return Task.FromResult<IEnumerable<ExhibitionResponse>>(result);
        }
    }

    public Task<ExhibitionResponse?> GetByIdAsync(int id)
    {
        lock (_lock)
        {
            var entity = _exhibitions.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(entity?.ToResponse());
        }
    }

    public Task<ExhibitionResponse> CreateAsync(ExhibitionCreateRequest request)
    {
        lock (_lock)
        {
            var newEntity = new Exhibition
            {
                Id = _nextId++,
                Name = request.Name,
                Theme = request.Theme,
                HallNumber = request.HallNumber,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };
            _exhibitions.Add(newEntity);
            return Task.FromResult(newEntity.ToResponse());
        }
    }

    public Task<bool> UpdateAsync(int id, ExhibitionUpdateRequest request)
    {
        lock (_lock)
        {
            var entity = _exhibitions.FirstOrDefault(e => e.Id == id);
            if (entity == null) return Task.FromResult(false);

            entity.Name = request.Name;
            entity.Theme = request.Theme;
            entity.HallNumber = request.HallNumber;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;

            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var entity = _exhibitions.FirstOrDefault(e => e.Id == id);
            if (entity == null) return Task.FromResult(false);

            _exhibitions.Remove(entity);
            return Task.FromResult(true);
        }
    }
}