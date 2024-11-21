using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases;

public class CreateUserHandler(IUserRepository userRepository)
{
    public async Task HandleAsync(CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.DisplayName))
            throw new ArgumentException("Display name cannot be empty.");

        var user = new User(Guid.NewGuid(), request.AuthProviderId, request.DisplayName);
        await userRepository.AddAsync(user);
    }
}

public abstract class CreateUserRequest
{
    public string DisplayName { get; set; }
    public string AuthProviderId { get; set; }
}