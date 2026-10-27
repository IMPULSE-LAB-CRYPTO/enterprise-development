using MuseumSystem.Domain.Enums;

namespace MuseumSystem.Contracts.Models;

public class TopExhibitionResponse
{
    public int ExhibitionId { get; set; }
    public string ExhibitionName { get; set; } = string.Empty;
    public int VisitorCount { get; set; }
}

public class ExcursionWithParticipantsResponse
{
    public int ExcursionId { get; set; }
    public DateTime StartsAt { get; set; }
    public int ParticipantCount { get; set; }
}

public class ThemeSummaryResponse
{
    public ExhibitionTheme Theme { get; set; }
    public int TotalVisitors { get; set; }
    public decimal TotalCost { get; set; }
    public double AvgVisitors { get; set; }
    public int MinVisitors { get; set; }
    public int MaxVisitors { get; set; }
}

public class ExcursionInHallResponse
{
    public int ExcursionId { get; set; }
    public DateTime StartsAt { get; set; }
    public TimeSpan Duration { get; set; }
}