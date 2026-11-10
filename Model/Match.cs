using System.ComponentModel.DataAnnotations;

namespace GoalStatsTrackerService.Model;

public class Match
{
    [Key]
    public string Id { get; set; } = "";
    public string TeamId { get; set; } = "";
    public string Opponent { get; set; } = "";
    /// <summary>Milliseconds since epoch.</summary>
    public long StartedAt { get; set; }
    /// <summary>Milliseconds since epoch. Null while the match is live.</summary>
    public long? EndedAt { get; set; }
    public int Period { get; set; } = 1;
    public List<string> PlayerIds { get; set; } = [];
    public List<Goal> Goals { get; set; } = [];
}
