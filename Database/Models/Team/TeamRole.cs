using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Models;

public class TeamRole
{
    [Required] public Guid RoleId { get; init; } = Guid.NewGuid();

    [Required] public string Name { get; init; } = string.Empty; // e.g., "Manager", "Owner", "Member"

    [Required] public int Permissions { get; set; } // Bitmask for permissions
}

public class TeamRoleConfiguration : IEntityTypeConfiguration<TeamRole>
{
    public void Configure(EntityTypeBuilder<TeamRole> builder)
    {
        builder.HasKey(x => x.RoleId);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Permissions)
            .IsRequired();

        // Seed default roles
        builder.HasData(
            new TeamRole
            {
                Name = "admin",
                Permissions = (int)(Permission.Edit | Permission.Delete | Permission.Invite | Permission.Remove) // 15
            },
            new TeamRole
            {
                Name = "manager",
                Permissions = (int)(Permission.Edit | Permission.Invite | Permission.Remove) // 13
            },
            new TeamRole
            {
                Name = "player",
                Permissions = (int)Permission.None // 0
            }
        );
    }
}

[Flags]
public enum Permission
{
    None = 0,
    Edit = 1 << 0, // 1
    Delete = 1 << 1, // 2
    Invite = 1 << 2, // 4
    Remove = 1 << 3 // 8
}