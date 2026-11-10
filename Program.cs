using GoalStatsTrackerService.Database;
using GoalStatsTrackerService.Model;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


var connectionString = "Data Source=/var/data/goaltracker/app.db";


Console.WriteLine("--------------------------------------------------------------------------------------------------");
var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("https://goal.teebee.no", "http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));


var app = builder.Build();
var logger = app.Logger;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/api/openapi/{documentName}.json");
    app.MapScalarApiReference("/api/scalar", options =>
        options.WithOpenApiRoutePattern("/api/openapi/{documentName}.json")
    );
}
app.UseCors("Frontend");
app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    // Performance boost: Enable Write-Ahead Logging for concurrent reads/writes
    // db.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL;"); 
}

app.MapGet("/api/health", () => Results.Ok("Healthy"));


#region Team Endpoints
app.MapGet("/teams", (AppDbContext db) => Results.Ok(db.Teams.ToList()));
app.MapGet("/teams/{id}", (AppDbContext db, string id) =>
{
    var team = db.Teams.FirstOrDefault(t => t.Id == id);
    return team == null ? Results.NotFound() : Results.Ok(team);
});
app.MapPut("/teams/{id}", (AppDbContext db, string id, Team updatedTeam) =>
{
    var team = db.Teams.FirstOrDefault(t => t.Id == id);
    if (team != null)
    {
        return Results.BadRequest("Team exists update with PATCH.");
    }

    team = new Team
    {
        Id = updatedTeam.Id,
        Name = updatedTeam.Name,
        Sport = updatedTeam.Sport,
        Players = updatedTeam.Players
    };
    db.SaveChanges();
    return Results.Ok(team);
});
app.MapPatch("/teams/{id}", (AppDbContext db, string id, Team updatedTeam) =>
{
    var team = db.Teams.FirstOrDefault(t => t.Id == id);
    if (team == null)
    {
        return Results.NotFound();
    }
    if (team.Id != updatedTeam.Id)
    {
        return Results.BadRequest("Team ID in the URL does not match the ID in the request body.");
    }

    team.Name = updatedTeam.Name ;
    team.Sport = updatedTeam.Sport;
    team.Players = updatedTeam.Players;

    db.SaveChanges();
    return Results.Ok(team);
});
#endregion

#region Match Endpoints
app.MapGet("/matches", (AppDbContext db) => Results.Ok(db.Matches.ToList()));
app.MapGet("/matches/{id}", (AppDbContext db, string id) =>
{
    var match = db.Matches.FirstOrDefault(m => m.Id.ToString() == id);
    return match == null ? Results.NotFound() : Results.Ok(match);
});
app.MapPut("/matches/{id}", (AppDbContext db, string id, Match updatedMatch) =>
{
    var match = db.Matches.FirstOrDefault(m => m.Id.ToString() == id);
    if (match != null)
    {
        return Results.BadRequest("Match exists update with PATCH.");
    }

    match = new Match
    {
        TeamId = updatedMatch.TeamId,
        Opponent = updatedMatch.Opponent,
        StartedAt = updatedMatch.StartedAt,
        Period = updatedMatch.Period,
        PlayerIds = updatedMatch.PlayerIds,
        Goals = updatedMatch.Goals
    };
    db.SaveChanges();
    return Results.Ok(match);
});
app.MapPatch("/matches/{id}", (AppDbContext db, string id, Match updatedMatch) =>
{
    var match = db.Matches.FirstOrDefault(m => m.Id.ToString() == id);
    if (match == null)
    {
        return Results.NotFound();
    }
    if (match.Id != updatedMatch.Id)
    {
        return Results.BadRequest("Match ID in the URL does not match the ID in the request body.");
    }

    match.TeamId = updatedMatch.TeamId;
    match.Opponent = updatedMatch.Opponent;
    match.StartedAt = updatedMatch.StartedAt;
    match.EndedAt = updatedMatch.EndedAt;
    match.Period = updatedMatch.Period;
    match.PlayerIds = updatedMatch.PlayerIds;
    match.Goals = updatedMatch.Goals;

    db.SaveChanges();
    return Results.Ok(match);
});
#endregion

#region Player Endpoints
app.MapGet("/players", (AppDbContext db) => Results.Ok(db.Players.ToList()));
app.MapPost("/teams/{id}/players", (string id, Player[] p, AppDbContext db) => {
    var team = db.Teams.FirstOrDefault(t => t.Id == id);
    if (team == null)
    {
        return Results.NotFound();
    }
    foreach (var player in p)
    {
        player.
        player.TeamId = id;
        db.Players.Add(player);
    }
    db.SaveChanges();
    return Results.Created($"/teams/{id}/players/{p.Id}", p);
});
#endregion

#region Goal Endpoints
app.MapGet("/goals", (AppDbContext db) => Results.Ok(db.Goals.ToList()));
#endregion


logger.LogInformation("Starting web application...");


app.Run();