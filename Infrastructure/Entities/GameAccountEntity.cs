namespace Database;

public class GameAccountEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Game { get; set; }
    public string AccountName { get; set; }
    public UserEntity User { get; set; }
}