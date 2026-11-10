namespace GoalStatsTrackerService.DTO;

public class Match
{
    public string Id { get; set; }
    public string TeamId { get; set; }
    public string Opponent { get; set; }
    public long StartedAt { get; set; }
    public long? EndedAt { get; set; }
    public int Period { get; set; }
    public List<string> PlayerIds { get; set; }
    public List<Goal> Goals { get; set; }
    
}