namespace GoalStatsTrackerService.DTO;

public class Team
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Sport { get; set; }
    public Player[] Players { get; set; }
}