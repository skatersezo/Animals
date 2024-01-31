using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Repositories;
using Animals.Core.Domain;
using Animals.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries.Handlers;

public class AnimalsQueryHandlerAsync : QueryHandlerAsync<AnimalsQuery, AnimalsQuery.Result>
{
    private readonly DbContextOptions<AnimalContext> _contextOptions;

    public AnimalsQueryHandlerAsync(
        DbContextOptions<AnimalContext> contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public override async Task<AnimalsQuery.Result> ExecuteAsync(AnimalsQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        await using var uow = new AnimalContext(_contextOptions);
        var repo = new AnimalRepositoryAsync<Animal>(uow);
        var animals = await repo.GetAllAsync(cancellationToken);

        var result = new AnimalsQuery.Result(animals.Select(a => new AnimalModel
        {
            Id = a.Id,
            Classification = a.Classification,
            Species = a.Species,
            Sound = a.Sound
        }).ToList());
        
        return result;
    }
}