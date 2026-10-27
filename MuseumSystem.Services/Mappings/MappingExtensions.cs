using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Services.Mappings;

public static class MappingExtensions
{
    public static ExhibitionResponse ToResponse(this Exhibition entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Theme = entity.Theme,
        HallNumber = entity.HallNumber,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate
    };

    public static ExcursionResponse ToResponse(this Excursion entity) => new()
    {
        Id = entity.Id,
        StartsAt = entity.StartsAt,
        Duration = TimeSpan.FromMinutes(entity.DurationMinutes), // ИСПРАВЛЕНО
        ExhibitionIds = entity.Exhibitions.Select(e => e.Id).ToList()
    };

    public static VisitorResponse ToResponse(this Visitor entity) => new()
    {
        Id = entity.Id,
        FullName = entity.FullName,
        Phone = entity.Phone,
        BirthDate = entity.BirthDate
    };

    public static TicketResponse ToResponse(this Ticket entity) => new()
    {
        Id = entity.Id,
        ExcursionId = entity.Excursion.Id,
        VisitorId = entity.Visitor.Id,
        Type = entity.Type,
        Price = entity.Price
    };
}