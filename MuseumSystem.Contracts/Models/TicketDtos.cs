using System.ComponentModel.DataAnnotations;
using MuseumSystem.Domain.Enums;

namespace MuseumSystem.Contracts.Models;

public class TicketResponse
{
    public int Id { get; set; }
    public int ExcursionId { get; set; }
    public int VisitorId { get; set; }
    public TicketType Type { get; set; }
    public decimal Price { get; set; }
}

public class TicketCreateRequest
{
    [Required]
    public int ExcursionId { get; set; }

    [Required]
    public int VisitorId { get; set; }

    [Required]
    public TicketType Type { get; set; }

    [Range(0, 1000000)]
    public decimal Price { get; set; }
}

public class TicketUpdateRequest
{
    [Required]
    public int ExcursionId { get; set; }

    [Required]
    public int VisitorId { get; set; }

    [Required]
    public TicketType Type { get; set; }

    [Range(0, 1000000)]
    public decimal Price { get; set; }
}