namespace GoalStatsTrackerService.DTO;

/// <summary>Backup file from the frontend. Importing replaces all data.</summary>
public record ImportDto(int Version, List<TeamDto> Teams, List<MatchDto> Matches);
