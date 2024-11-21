namespace Database;

public class TeamEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Game { get; set; }
    public ICollection<TeamMemberEntity> Members { get; set; }
}