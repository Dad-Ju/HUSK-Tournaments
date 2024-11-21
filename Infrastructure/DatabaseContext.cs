using Microsoft.EntityFrameworkCore;

namespace Database;

public class DatabaseContext(string connectionString) : DbContext
{
    public DatabaseContext() : this(GetDefaultConnectionString())
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<GameAccountEntity> GameAccounts { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }
    public DbSet<TeamEntity> Teams { get; set; }
    public DbSet<TeamMemberEntity> TeamMembers { get; set; }
    public DbSet<TeamInviteEntity> TeamInvites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AuthProviderId).IsRequired();
            entity.HasMany(e => e.GameAccounts).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            entity.HasMany(e => e.Roles).WithOne(e => e.User).HasForeignKey(e => e.UserId);
        });

        modelBuilder.Entity<GameAccountEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Game).IsRequired();
            entity.Property(e => e.AccountName).IsRequired();
        });

        modelBuilder.Entity<UserRoleEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Permissions).IsRequired();
        });

        modelBuilder.Entity<TeamEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Game).IsRequired();
        });

        modelBuilder.Entity<TeamMemberEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Role).IsRequired();
            entity.Property(e => e.Permissions).IsRequired();
        });

        modelBuilder.Entity<TeamInviteEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Team).WithMany().HasForeignKey(e => e.TeamId);
            entity.HasOne(e => e.InvitedUser).WithMany().HasForeignKey(e => e.InvitedUserId);
            entity.HasOne(e => e.InvitingUser).WithMany().HasForeignKey(e => e.InvitingUserId);
        });
    }

    private static string GetDefaultConnectionString()
    {
        var dbPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Data",
            "tournaments.db"
        );

        Directory.CreateDirectory(Path.GetDirectoryName(dbPath));

        return $"Data Source={dbPath}";
    }

    public static class DatabaseInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            // Apply migrations and create the database if it doesn't exist
            context.Database.Migrate();
        }
    }
}