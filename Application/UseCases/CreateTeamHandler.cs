using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases;

public class CreateTeamHandler(ITeamRepository teamRepository)
{
    public async Task HandleAsync(CreateTeamRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Team name cannot be empty.");

        var team = new Team(Guid.NewGuid(), request.Name, request.Game);
        await teamRepository.AddAsync(team);
    }
}

public abstract class CreateTeamRequest
{
    public string Name { get; set; }
    public string Game { get; set; }
}