using GoalStatsTrackerService.Model;

namespace GoalStatsTrackerService.DTO;

public record GoalDto(string Id, Side Side, string? ScorerId, string? AssistId, long At, int Period);

public record GoalPatchDto(Optional<string?> ScorerId, Optional<string?> AssistId, Side? Side, int? Period, long? At);

public static class GoalMapping
{
    public static GoalDto ToDto(this Goal goal) =>
        new(goal.Id, goal.Side, goal.ScorerId, goal.AssistId, goal.At, goal.Period);

    public static void Update(this Goal goal, GoalDto dto)
    {
        goal.Side = dto.Side;
        goal.ScorerId = dto.ScorerId;
        goal.AssistId = dto.AssistId;
        goal.At = dto.At;
        goal.Period = dto.Period;
    }

    public static void Apply(this Goal goal, GoalPatchDto patch)
    {
        if (patch.ScorerId.HasValue) goal.ScorerId = patch.ScorerId.Value;
        if (patch.AssistId.HasValue) goal.AssistId = patch.AssistId.Value;
        if (patch.Side != null) goal.Side = patch.Side.Value;
        if (patch.Period != null) goal.Period = patch.Period.Value;
        if (patch.At != null) goal.At = patch.At.Value;
    }
}
