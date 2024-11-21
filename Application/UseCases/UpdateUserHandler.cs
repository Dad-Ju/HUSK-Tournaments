using Application.Interfaces;

namespace Application.UseCases;

public class
    UpdateUserHandler(IUserRepository userRepository)
{
    public async Task HandleAsync(UpdateUserRequest request)
    {
        var user = await userRepository.GetByIdAsync(request.Id);
        if (user == null)
            throw new InvalidOperationException("User not found.");

        user.UpdateDisplayName(request.DisplayName);
        await userRepository.UpdateAsync(user);
    }
}

public abstract class UpdateUserRequest
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
}