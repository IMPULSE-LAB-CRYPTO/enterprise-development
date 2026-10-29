using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;

namespace MuseumSystem.Infrastructure.InMemory.Repositories;

public class InMemoryTicketRepository : ITicketRepository
{
    private readonly List<Ticket> _tickets;
    private readonly IExcursionRepository _excursionRepository;
    private readonly IVisitorRepository _visitorRepository;
    private readonly object _lock = new();
    private int _nextId;

    public InMemoryTicketRepository(IExcursionRepository excursionRepository, IVisitorRepository visitorRepository)
    {
        _excursionRepository = excursionRepository;
        _visitorRepository = visitorRepository;

        var excursions = _excursionRepository.GetAllAsync().Result.ToList();
        var visitors = _visitorRepository.GetAllAsync().Result.ToList();

        _tickets = MuseumDataSeeder.GetTickets(excursions, visitors);
        _nextId = _tickets.Count > 0 ? _tickets.Max(t => t.Id) + 1 : 1;
    }

    public Task<IEnumerable<Ticket>> GetAllAsync()
    {
        lock (_lock) return Task.FromResult<IEnumerable<Ticket>>(_tickets.ToList());
    }

    public Task<Ticket?> GetByIdAsync(int id)
    {
        lock (_lock) return Task.FromResult(_tickets.FirstOrDefault(t => t.Id == id));
    }

    public Task<Ticket> AddAsync(Ticket ticket)
    {
        lock (_lock)
        {
            ticket.Id = _nextId++;
            _tickets.Add(ticket);
            return Task.FromResult(ticket);
        }
    }

    public Task<Ticket?> UpdateAsync(Ticket ticket)
    {
        lock (_lock)
        {
            var existing = _tickets.FirstOrDefault(t => t.Id == ticket.Id);
            if (existing == null) return Task.FromResult<Ticket?>(null);

            existing.Excursion = ticket.Excursion;
            existing.Visitor = ticket.Visitor;
            existing.Type = ticket.Type;
            existing.Price = ticket.Price;

            return Task.FromResult<Ticket?>(existing);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock)
        {
            var existing = _tickets.FirstOrDefault(t => t.Id == id);
            if (existing == null) return Task.FromResult(false);

            _tickets.Remove(existing);
            return Task.FromResult(true);
        }
    }
}