using System.ComponentModel.DataAnnotations;

namespace GoalStatsTrackerService.Model;

public record Goal
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool OurGoal { get; set; } = false;
    public Guid? ScorerId { get; set; } = null;
    public Guid? AssistId { get; set; } = null;
    public long ScoredAt { get; set; } =  DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public int Period { get; set; } = 1;
}