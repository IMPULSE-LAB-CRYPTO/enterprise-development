using System.ComponentModel.DataAnnotations;

namespace MuseumSystem.Contracts.Models;

public class ExcursionResponse
{
    public int Id { get; set; }
    public DateTime StartsAt { get; set; }
    public TimeSpan Duration { get; set; }
    public List<int> ExhibitionIds { get; set; } = new();
}

public class ExcursionCreateRequest
{
    [Required]
    public DateTime StartsAt { get; set; }

    [Required]
    public TimeSpan Duration { get; set; }

    public List<int> ExhibitionIds { get; set; } = new();
}

public class ExcursionUpdateRequest
{
    [Required]
    public DateTime StartsAt { get; set; }

    [Required]
    public TimeSpan Duration { get; set; }

    public List<int> ExhibitionIds { get; set; } = new();
}