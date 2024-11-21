namespace Database;

public class UserEntity
{
    public Guid Id { get; set; }
    public string AuthProviderId { get; set; }
    public string DisplayName { get; set; }
    public ICollection<GameAccountEntity> GameAccounts { get; set; }
    public ICollection<UserRoleEntity> Roles { get; set; }
}