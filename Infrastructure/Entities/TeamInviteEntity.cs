namespace Database;

public class TeamInviteEntity
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public Guid InvitedUserId { get; set; }
    public Guid InvitingUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public TeamEntity Team { get; set; }
    public UserEntity InvitedUser { get; set; }
    public UserEntity InvitingUser { get; set; }
}