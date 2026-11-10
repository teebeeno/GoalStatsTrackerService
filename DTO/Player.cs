namespace GoalStatsTrackerService.DTO;

public class Player
{
    public string Id { get; set; }
    public string Name { get; set; }  
    public int Number { get; set; }
    public bool? Archived { get; set; }
}