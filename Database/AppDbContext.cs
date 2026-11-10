using GoalStatsTrackerService.Model;
using Microsoft.EntityFrameworkCore;

namespace GoalStatsTrackerService.Database;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Team> Teams => Set<Team>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Store enums as readable strings instead of ints
        configurationBuilder.Properties<Sport>().HaveConversion<string>();
        configurationBuilder.Properties<Side>().HaveConversion<string>();
    }
}
