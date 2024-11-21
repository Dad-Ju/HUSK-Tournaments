using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases;

public class GetUserHandler(IUserRepository userRepository)
{
    public async Task<User> HandleAsync(Guid id)
    {
        return await userRepository.GetByIdAsync(id);
    }
}