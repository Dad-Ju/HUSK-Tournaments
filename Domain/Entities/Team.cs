namespace Domain.Entities;

public class Team(Guid id, string name, string game)
{
    private readonly List<TeamMember> _members = [];

    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name ?? throw new ArgumentNullException(nameof(name));
    public string Game { get; private set; } = game ?? throw new ArgumentNullException(nameof(game));

    public IReadOnlyCollection<TeamMember> Members => _members.AsReadOnly();

    public void AddMember(TeamMember member)
    {
        if (_members.Any(m => m.UserId == member.UserId))
            throw new InvalidOperationException("User is already a team member.");

        _members.Add(member);
    }
}