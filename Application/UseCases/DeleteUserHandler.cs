using Application.Interfaces;

namespace Application.UseCases;

public class DeleteUserHandler(IUserRepository userRepository)
{
    public async Task HandleAsync(Guid id)
    {
        await userRepository.DeleteAsync(id);
    }
}