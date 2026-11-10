using GoalStatsTrackerService.Model;

namespace GoalStatsTrackerService.DTO;

public record PlayerDto(string Id, string Name, string? Number, bool? Archived);

public record PlayerPatchDto(string? Name, Optional<string?> Number, Optional<bool?> Archived);

public static class PlayerMapping
{
    public static PlayerDto ToDto(this Player player) =>
        new(player.Id, player.Name, player.Number, player.Archived);

    public static void Update(this Player player, PlayerDto dto)
    {
        player.Name = dto.Name;
        player.Number = dto.Number;
        player.Archived = dto.Archived ?? false;
    }

    public static void Apply(this Player player, PlayerPatchDto patch)
    {
        if (patch.Name != null) player.Name = patch.Name;
        if (patch.Number.HasValue) player.Number = patch.Number.Value;
        if (patch.Archived.HasValue) player.Archived = patch.Archived.Value ?? false;
    }

    /// <summary>Adds new players and updates existing ones, matched by id.</summary>
    public static void UpsertPlayers(this Team team, IEnumerable<PlayerDto> players)
    {
        foreach (var dto in players)
        {
            var player = team.Players.FirstOrDefault(p => p.Id == dto.Id);
            if (player == null)
            {
                player = new Player { Id = dto.Id, TeamId = team.Id };
                team.Players.Add(player);
            }
            player.Update(dto);
        }
    }
}
