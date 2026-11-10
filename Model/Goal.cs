using Microsoft.EntityFrameworkCore;

namespace GoalStatsTrackerService.Model;

[PrimaryKey(nameof(MatchId), nameof(Id))]
public class Goal
{
    public string MatchId { get; set; } = "";
    public string Id { get; set; } = "";
    public Side Side { get; set; }
    public string? ScorerId { get; set; }
    public string? AssistId { get; set; }
    /// <summary>Wall-clock time of the goal, milliseconds since epoch.</summary>
    public long At { get; set; }
    public int Period { get; set; } = 1;
}
