using GoalStatsTrackerService.Model;

namespace GoalStatsTrackerService.DTO;

public record TeamDto(string Id, string Name, Sport Sport, List<PlayerDto> Players);

public record TeamPatchDto(string? Name, Sport? Sport);

public static class TeamMapping
{
    public static TeamDto ToDto(this Team team) =>
        new(team.Id, team.Name, team.Sport, team.Players.Select(p => p.ToDto()).ToList());

    public static Team ToModel(this TeamDto dto, string id)
    {
        var team = new Team { Id = id };
        team.Update(dto);
        return team;
    }

    /// <summary>Full replace: players missing from <paramref name="dto"/> are removed.</summary>
    public static void Update(this Team team, TeamDto dto)
    {
        team.Name = dto.Name;
        team.Sport = dto.Sport;
        team.Players.RemoveAll(p => dto.Players.All(d => d.Id != p.Id));
        team.UpsertPlayers(dto.Players);
    }

    public static void Apply(this Team team, TeamPatchDto patch)
    {
        if (patch.Name != null) team.Name = patch.Name;
        if (patch.Sport != null) team.Sport = patch.Sport.Value;
    }
}
