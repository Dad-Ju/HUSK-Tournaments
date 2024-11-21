using System.Reflection;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
        var folder = Environment.SpecialFolder.ApplicationData;
        var path = Path.Combine(Environment.GetFolderPath(folder), "HUSK-Tournaments");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        DbPath = Path.Join(path, "tournament.db");
    }

    private string DbPath { get; }

    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerAccount> PlayerAccounts { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamRole> TeamRoles { get; set; }
    public DbSet<PlayerTeamRole> PlayerTeamRoles { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<Match> Matches { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@$"Data Source={DbPath}");
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}