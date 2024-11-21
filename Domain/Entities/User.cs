namespace Domain.Entities;

public class User(Guid id, string authProviderId, string displayName)
{
    public Guid Id { get; private set; } = id;

    public string AuthProviderId { get; private set; } =
        authProviderId ?? throw new ArgumentNullException(nameof(authProviderId));

    public string DisplayName { get; private set; } =
        displayName ?? throw new ArgumentNullException(nameof(displayName));

    public void UpdateDisplayName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Display name cannot be empty.");

        DisplayName = newName;
    }
}