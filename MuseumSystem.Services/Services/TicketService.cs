using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Services.Mappings;

namespace MuseumSystem.Services.Services;

public class TicketService : ITicketService
{
    private readonly List<Ticket> _tickets;
    private readonly List<Excursion> _excursions;
    private readonly List<Visitor> _visitors;
    private readonly object _lock = new();
    private int _nextId;

    public TicketService()
    {
        // ИСПРАВЛЕНО: передаем нужные списки
        var exhibitions = MuseumDataSeeder.GetExhibitions();
        _excursions = MuseumDataSeeder.GetExcursions(exhibitions);
        _visitors = MuseumDataSeeder.GetVisitors();
        _tickets = MuseumDataSeeder.GetTickets(_excursions, _visitors);

        _nextId = _tickets.Any() ? _tickets.Max(t => t.Id) + 1 : 1;
    }

    public Task<IEnumerable<TicketResponse>> GetAllAsync()
    {
        lock (_lock)
        {
            var result = _tickets.Select(t => t.ToResponse()).ToList();
            return Task.FromResult<IEnumerable<TicketResponse>>(result);
        }
    }

    public Task<TicketResponse?> GetByIdAsync(int id)
    {
        lock (_lock)
        {
            var entity = _tickets.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(entity?.ToResponse());
        }
    }

    public Task<TicketResponse> CreateAsync(TicketCreateRequest request)
    {
        lock (_lock)
        {
            var excursion = _excursions.FirstOrDefault(e => e.Id == request.ExcursionId);
            var visitor = _visitors.FirstOrDefault(v => v.Id == request.VisitorId);

            if (excursion == null || visitor == null)
                throw new ArgumentException("Экскурсия или посетитель не найдены");

            var newEntity = new Ticket
            {
                Id = _nextId++,
                Excursion = excursion,
                Visitor = visitor,
                Type = request.Type,
                Price = request.Price
            };
            _tickets.Add(newEntity);
            return Task.FromResult(newEntity.ToResponse());
        }
    }

    public Task<bool> UpdateAsync(int id, TicketUpdateRequest request)
    {
        lock (_lock)
        {
            var entity = _tickets.FirstOrDefault(t => t.Id == id);
            if (entity == null) return Task.FromResult(false);

            var excursion = _excursions.FirstOrDefault(e => e.Id == request.ExcursionId);
            var visitor = _visitors.FirstOrDefault(v => v.Id == request.VisitorId);

            if (excursion == null || visitor == null) return Task.FromResult(false);

            entity.Excursion = excursion;
            entity.Visitor = visitor;
            entity.Type = request.Type;
            entity.Price = request.Price;

            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var entity = _tickets.FirstOrDefault(t => t.Id == id);
            if (entity == null) return Task.FromResult(false);

            _tickets.Remove(entity);
            return Task.FromResult(true);
        }
    }
}