using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Models;

[Table("teams")]
public class Team
{
    [Required] public Guid TeamId { get; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string Game { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }

    public ICollection<Player> Players { get; } = new List<Player>();
    public ICollection<PlayerTeamRole> PlayerTeamRoles { get; } = new List<PlayerTeamRole>();

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; }
}

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(x => x.TeamId);

        builder.Property(x => x.TeamId)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.IsDisabled)
            .IsRequired()
            .HasDefaultValue(false);
    }
}