using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Models;

public class PlayerTeamRole
{
    [Required] public Guid PlayerId { get; init; }
    public Player Player { get; } = null!;

    [Required] public Guid TeamId { get; init; }
    public Team Team { get; } = null!;

    [Required] public Guid RoleId { get; init; }
    public TeamRole Role { get; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; }

    public class PlayerTeamRoleConfiguration : IEntityTypeConfiguration<PlayerTeamRole>
    {
        public void Configure(EntityTypeBuilder<PlayerTeamRole> builder)
        {
            builder.HasKey(x => new { x.PlayerId, x.TeamId, x.RoleId }); // Composite Key

            builder.HasOne(x => x.Player)
                .WithMany()
                .HasForeignKey(x => x.PlayerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Team)
                .WithMany(x => x.PlayerTeamRoles)
                .HasForeignKey(x => x.TeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting roles that are in use
        }
    }
}