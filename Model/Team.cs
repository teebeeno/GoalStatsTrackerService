using System.ComponentModel.DataAnnotations;

namespace GoalStatsTrackerService.Model;

public class Team
{
    [Key]
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public Sport Sport { get; set; }
    public List<Player> Players { get; set; } = [];
    public List<Match> Matches { get; set; } = [];
}
