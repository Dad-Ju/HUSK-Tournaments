namespace Database;

[Flags]
public enum TeamPermissionFlags
{
    None = 0,
    Edit = 1 << 0,
    Delete = 1 << 1,
    Invite = 1 << 2,
    Remove = 1 << 3
}

public class TeamMemberEntity
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; }
    public TeamPermissionFlags Permissions { get; set; }
    public TeamEntity Team { get; set; }
    public UserEntity User { get; set; }
}