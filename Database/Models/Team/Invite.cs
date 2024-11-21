using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Models;

[Table("invites")]
public class Invite
{
    [Required] public Guid InviteId { get; init; } = Guid.NewGuid();

    [Required] public InviteStatus Status { get; set; } = InviteStatus.Pending;

    [Required] public Guid TeamId { get; init; }
    [Required] public Team Team { get; } = null!;

    [Required] public Guid TargetId { get; init; }
    [Required] public Player Target { get; } = null!;

    [Required] public Guid SenderId { get; init; }
    [Required] public Player Sender { get; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; }
}

public class InviteConfiguration : IEntityTypeConfiguration<Invite>
{
    public void Configure(EntityTypeBuilder<Invite> builder)
    {
        builder.HasKey(x => x.InviteId);

        builder.Property(x => x.InviteId)
            .IsRequired()
            .HasMaxLength(255);

        // Relationship to Sender
        builder.HasOne(x => x.Sender)
            .WithMany() // No collection in Player
            .HasForeignKey(x => x.SenderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Relationship to Target
        builder.HasOne(x => x.Target)
            .WithMany() // No collection in Player
            .HasForeignKey(x => x.TargetId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Relationship to Team
        builder.HasOne(x => x.Team)
            .WithMany()
            .HasForeignKey(x => x.TeamId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public enum InviteStatus
{
    Pending,
    Accepted,
    Declined
}