namespace GoalStatsTrackerService.DTO;

public class Goal
{
    public string Id { get; set; }
    public string? ScorerId { get; set; }
    public string? AssistId { get; set; }
    public DateTime At { get; set; }
    public int Period { get; set; }
    
}