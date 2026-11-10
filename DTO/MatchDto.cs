using GoalStatsTrackerService.Model;

namespace GoalStatsTrackerService.DTO;

public record MatchDto(
    string Id,
    string TeamId,
    string Opponent,
    long StartedAt,
    long? EndedAt,
    int Period,
    List<string> PlayerIds,
    List<GoalDto> Goals);

public record MatchPatchDto(string? Opponent, int? Period, Optional<long?> EndedAt, List<string>? PlayerIds);

public static class MatchMapping
{
    public static MatchDto ToDto(this Match match) =>
        new(
            match.Id,
            match.TeamId,
            match.Opponent,
            match.StartedAt,
            match.EndedAt,
            match.Period,
            match.PlayerIds,
            match.Goals.OrderBy(g => g.At).Select(g => g.ToDto()).ToList());

    public static Match ToModel(this MatchDto dto, string id)
    {
        var match = new Match { Id = id };
        match.Update(dto);
        return match;
    }

    /// <summary>Full replace: goals missing from <paramref name="dto"/> are removed.</summary>
    public static void Update(this Match match, MatchDto dto)
    {
        match.TeamId = dto.TeamId;
        match.Opponent = dto.Opponent;
        match.StartedAt = dto.StartedAt;
        match.EndedAt = dto.EndedAt;
        match.Period = dto.Period;
        match.PlayerIds = dto.PlayerIds;

        match.Goals.RemoveAll(g => dto.Goals.All(d => d.Id != g.Id));
        foreach (var goalDto in dto.Goals)
        {
            match.UpsertGoal(goalDto.Id).Update(goalDto);
        }
    }

    public static void Apply(this Match match, MatchPatchDto patch)
    {
        if (patch.Opponent != null) match.Opponent = patch.Opponent;
        if (patch.Period != null) match.Period = patch.Period.Value;
        if (patch.EndedAt.HasValue) match.EndedAt = patch.EndedAt.Value;
        if (patch.PlayerIds != null) match.PlayerIds = patch.PlayerIds;
    }

    /// <summary>Returns the goal with <paramref name="goalId"/>, adding a new one if it doesn't exist.</summary>
    public static Goal UpsertGoal(this Match match, string goalId)
    {
        var goal = match.Goals.FirstOrDefault(g => g.Id == goalId);
        if (goal == null)
        {
            goal = new Goal { Id = goalId, MatchId = match.Id };
            match.Goals.Add(goal);
        }
        return goal;
    }
}
