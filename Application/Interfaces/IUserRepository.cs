using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
}