using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Services.Mappings;

namespace MuseumSystem.Services.Services;

public class VisitorService : IVisitorService
{
    private readonly List<Visitor> _visitors;
    private readonly object _lock = new();
    private int _nextId;

    public VisitorService()
    {
        _visitors = MuseumDataSeeder.GetVisitors();
        _nextId = _visitors.Any() ? _visitors.Max(v => v.Id) + 1 : 1;
    }

    public Task<IEnumerable<VisitorResponse>> GetAllAsync()
    {
        lock (_lock)
        {
            var result = _visitors.Select(v => v.ToResponse()).ToList();
            return Task.FromResult<IEnumerable<VisitorResponse>>(result);
        }
    }

    public Task<VisitorResponse?> GetByIdAsync(int id)
    {
        lock (_lock)
        {
            var entity = _visitors.FirstOrDefault(v => v.Id == id);
            return Task.FromResult(entity?.ToResponse());
        }
    }

    public Task<VisitorResponse> CreateAsync(VisitorCreateRequest request)
    {
        lock (_lock)
        {
            var newEntity = new Visitor
            {
                Id = _nextId++,
                FullName = request.FullName,
                Phone = request.Phone,
                BirthDate = request.BirthDate
            };
            _visitors.Add(newEntity);
            return Task.FromResult(newEntity.ToResponse());
        }
    }

    public Task<bool> UpdateAsync(int id, VisitorUpdateRequest request)
    {
        lock (_lock)
        {
            var entity = _visitors.FirstOrDefault(v => v.Id == id);
            if (entity == null) return Task.FromResult(false);

            entity.FullName = request.FullName;
            entity.Phone = request.Phone;
            entity.BirthDate = request.BirthDate;

            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var entity = _visitors.FirstOrDefault(v => v.Id == id);
            if (entity == null) return Task.FromResult(false);

            _visitors.Remove(entity);
            return Task.FromResult(true);
        }
    }
}