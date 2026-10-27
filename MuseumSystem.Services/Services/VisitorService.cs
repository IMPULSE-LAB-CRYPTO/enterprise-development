using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Services.Mappings;

namespace MuseumSystem.Services.Services;

public class VisitorService : IVisitorService
{
    private readonly ILogger<VisitorService> _logger;
    private readonly List<Visitor> _visitors;
    private readonly object _lock = new();
    private int _nextId;

    public VisitorService() : this(NullLogger<VisitorService>.Instance) { }

    public VisitorService(ILogger<VisitorService> logger)
    {
        _logger = logger;
        _visitors = MuseumDataSeeder.GetVisitors();
        _nextId = _visitors.Count > 0 ? _visitors.Max(v => v.Id) + 1 : 1;
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
            _logger.LogInformation("Создание посетителя: {FullName}", request.FullName);
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
            if (entity == null)
            {
                _logger.LogWarning("Посетитель с ID {Id} не найден", id);
                return Task.FromResult(false);
            }

            _logger.LogInformation("Обновление посетителя {Id}", id);
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
            if (entity == null)
            {
                _logger.LogWarning("Посетитель с ID {Id} не найден для удаления", id);
                return Task.FromResult(false);
            }

            _logger.LogInformation("Удаление посетителя {Id}", id);
            _visitors.Remove(entity);
            return Task.FromResult(true);
        }
    }
}