using System.ComponentModel.DataAnnotations;

namespace GoalStatsTrackerService.Model;

public class Match
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TeamId { get; set; }
    public string Opponent { get; set; }
    public long StartedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public long? EndedAt { get; set; } = null;
    public int Period { get; set; } = 1;
    public List<Guid> PlayerIds { get; set; } = [];
    public List<Goal> Goals { get; set; } = [];
}