namespace Domain.Entities;

[Flags]
public enum TeamPermissionFlags
{
    None = 0,
    Edit = 1 << 0,
    Delete = 1 << 1,
    Invite = 1 << 2,
    Remove = 1 << 3
}

public abstract class TeamMember(Guid id, Guid userId, string role, TeamPermissionFlags permissions)
{
    public Guid Id { get; private set; } = id;
    public Guid UserId { get; private set; } = userId;
    public string Role { get; private set; } = role ?? throw new ArgumentNullException(nameof(role));
    public TeamPermissionFlags Permissions { get; private set; } = permissions;
}