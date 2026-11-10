using System.ComponentModel.DataAnnotations;

namespace GoalStatsTrackerService.Model;

public class Team
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Sport { get; set; }
    //public Player[] Players { get; set; }
}