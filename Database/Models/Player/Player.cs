using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Models;

[Table("players")]
public class Player
{
    [Required] public Guid PlayerId { get; init; } = Guid.NewGuid();

    [Required] public string DiscordId { get; init; }

    [Required] public string Description { get; set; } = string.Empty;

    public Guid TeamId { get; set; } = Guid.Empty;
    public Team Team { get; } = null!;

    public ICollection<PlayerAccount> Accounts { get; } = new List<PlayerAccount>();


    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; }

    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(x => x.PlayerId);

            builder.Property(x => x.PlayerId)
                .IsRequired();

            builder.Property(x => x.DiscordId)
                .IsRequired()
                .HasMaxLength(255);

            // Relationship with Team
            builder.HasOne(x => x.Team)
                .WithMany(x => x.Players) // Many players in a team
                .HasForeignKey(x => x.TeamId) // Foreign key in Player
                .OnDelete(DeleteBehavior.Restrict); // Prevent team deletion if players exist

            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}