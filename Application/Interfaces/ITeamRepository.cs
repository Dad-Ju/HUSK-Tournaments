using Domain.Entities;

namespace Application.Interfaces;

public interface ITeamRepository
{
    Task AddAsync(Team team);
    Task<Team> GetByIdAsync(Guid id);
    Task UpdateAsync(Team team);
    Task DeleteAsync(Guid id);
}