namespace Database;

[Flags]
public enum GlobalPermissionFlags
{
    None = 0,
    Admin = 1 << 0,
    Moderator = 1 << 1,
    Organizer = 1 << 2,
    Player = 1 << 3
}

public class UserRoleEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public GlobalPermissionFlags Permissions { get; set; }
    public UserEntity User { get; set; }
}