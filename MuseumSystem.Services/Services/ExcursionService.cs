using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Services.Mappings;

namespace MuseumSystem.Services.Services;

public class ExcursionService : IExcursionService
{
    private readonly ILogger<ExcursionService> _logger;
    private readonly List<Excursion> _excursions;
    private readonly List<Exhibition> _exhibitions;
    private readonly object _lock = new();
    private int _nextId;

    public ExcursionService() : this(NullLogger<ExcursionService>.Instance) { }

    public ExcursionService(ILogger<ExcursionService> logger)
    {
        _logger = logger;
        _exhibitions = MuseumDataSeeder.GetExhibitions();
        _excursions = MuseumDataSeeder.GetExcursions(_exhibitions);
        _nextId = _excursions.Count > 0 ? _excursions.Max(e => e.Id) + 1 : 1;
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
            _logger.LogInformation("Создание экскурсии на {StartsAt}", request.StartsAt);
            var exhibitions = _exhibitions.Where(e => request.ExhibitionIds.Contains(e.Id)).ToList();
            var newEntity = new Excursion
            {
                Id = _nextId++,
                StartsAt = request.StartsAt,
                DurationMinutes = (int)request.Duration.TotalMinutes,
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
            if (entity == null)
            {
                _logger.LogWarning("Экскурсия с ID {Id} не найдена", id);
                return Task.FromResult(false);
            }

            _logger.LogInformation("Обновление экскурсии {Id}", id);
            entity.StartsAt = request.StartsAt;
            entity.DurationMinutes = (int)request.Duration.TotalMinutes;
            entity.Exhibitions = _exhibitions.Where(e => request.ExhibitionIds.Contains(e.Id)).ToList();

            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var entity = _excursions.FirstOrDefault(e => e.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("Экскурсия с ID {Id} не найдена для удаления", id);
                return Task.FromResult(false);
            }

            _logger.LogInformation("Удаление экскурсии {Id}", id);
            _excursions.Remove(entity);
            return Task.FromResult(true);
        }
    }
}