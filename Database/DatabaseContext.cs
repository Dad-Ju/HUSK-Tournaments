using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Database;

public class DatabaseContext : DbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerAccount> PlayerAccounts { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<Team> Teams { get; set; }

    private IConfiguration? _configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables(prefix: "DB_")
            // ReSharper disable once StringLiteralTypo
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        if (string.IsNullOrWhiteSpace(_configuration["connectionString"]))
        {
            throw new NullReferenceException("ConnectionString is missing");
        }

        optionsBuilder.UseSqlite(@$"Data Source={_configuration["connectionString"]}");
    }
}