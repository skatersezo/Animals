using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Repositories;
using Animals.Core.Domain;
using Animals.Core.Domain.Models;
using Animals.Core.Ports.Exceptions;
using Microsoft.EntityFrameworkCore;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries.Handlers;

public class DogByIdQueryHandlerAsync : QueryHandlerAsync<DogByIdQuery, DogByIdQuery.Result>
{
    private readonly DbContextOptions<AnimalContext> _contextOptions;

    public DogByIdQueryHandlerAsync(DbContextOptions<AnimalContext> contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public override async Task<DogByIdQuery.Result> ExecuteAsync(DogByIdQuery query, CancellationToken cancellationToken = new())
    {
        await using var uow = new AnimalContext(_contextOptions);
        var repo = new AnimalRepositoryAsync<Dog>(uow);
        var dog = await repo.GetAsync(query.DogId, cancellationToken);

        if (dog is null)
            throw new DogNotFoundException(query.DogId);

        return new DogByIdQuery.Result(new DogModel
        {
            Id = dog.Id,
            Classification = dog.Classification,
            Name = dog.Name,
            Species = dog.Species,
            Sound = dog.Sound
        });
    }
}