using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Models;

public class Match
{
    [Required] public Guid MatchId { get; init; } = Guid.NewGuid();

    // Team A
    [Required] public Guid TeamAId { get; init; }
    public Team TeamA { get; set; } = null!;

    // Team B
    [Required] public Guid TeamBId { get; init; }
    public Team TeamB { get; set; } = null!;

    [Required] public DateTime MatchDate { get; set; }

    // Optional properties, such as match results
    public string? Result { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; }

    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(x => x.MatchId);

            builder.Property(x => x.MatchId)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.MatchDate)
                .IsRequired();

            // Relationship to Team A
            builder.HasOne(x => x.TeamA)
                .WithMany() // No navigation from Team to Matches
                .HasForeignKey(x => x.TeamAId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship to Team B
            builder.HasOne(x => x.TeamB)
                .WithMany() // No navigation from Team to Matches
                .HasForeignKey(x => x.TeamBId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}