using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Repositories;
using Animals.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries.Handlers;

public class AnimalsQueryHandlerAsync : QueryHandlerAsync<AnimalsQuery, AnimalsQuery.Result>
{
    private readonly DbContextOptions<AnimalContext> _contextOptions;

    public AnimalsQueryHandlerAsync(DbContextOptions<AnimalContext> contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public override async Task<AnimalsQuery.Result> ExecuteAsync(AnimalsQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        List<Animal> animals = new List<Animal>();

        await using var uow = new AnimalContext(_contextOptions);
        var dogsRepo = new DogRepositoryAsync(uow);
        var catsRepo = new CatRepositoryAsync(uow);
        var pigeonsRepo = new PigeonRepositoryAsync(uow);
            
        animals.AddRange(await dogsRepo.GetAllAsync(cancellationToken));
        animals.AddRange(await catsRepo.GetAllAsync(cancellationToken));
        animals.AddRange(await pigeonsRepo.GetAllAsync(cancellationToken));

        return new AnimalsQuery.Result(animals.Select(a => new AnimalByIdQuery.Result<Animal>(a)));

    }
}