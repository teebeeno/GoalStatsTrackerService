using System.ComponentModel.DataAnnotations;

namespace GoalStatsTrackerService.Model;

public class Player
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public int? Number { get; set; }
    public bool? Active { get; set; } = false; 
    public Guid? TeamId { get; set; }
    
}