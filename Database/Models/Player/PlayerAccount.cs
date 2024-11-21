using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Models;

[Table("player_accounts")]
public class PlayerAccount
{
    [Required] public Guid PlayerAccountId { get; init; } = Guid.NewGuid();
    [Required] public Guid PlayerId { get; init; }
    [Required] public Player Player { get; } = null!;

    [Required] public PlayerAccountPlatforms Platform { get; init; }

    [Required] public List<PlayerAccountGames> Game { get; init; } = new();

    [Required] public string PlatformIdentifier { get; set; } = string.Empty;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; }
}

public class PlayerAccountConfiguration : IEntityTypeConfiguration<PlayerAccount>
{
    public void Configure(EntityTypeBuilder<PlayerAccount> builder)
    {
        builder.HasKey(x => x.PlayerAccountId);
    }
}

public enum PlayerAccountPlatforms
{
    Riot,
    Steam,
    Faceit,
    Uplay,
    EA
}

public enum PlayerAccountGames
{
    Valorant,
    Faceit,
    CounterStrike
}