using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Repositories;
using Animals.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries.Handlers;

public class DogByNameQueryHandlerAsync : QueryHandlerAsync<DogByNameQuery, DogByNameQuery.Result>
{
    private readonly DbContextOptions<AnimalContext> _contextOptions;
    
    public DogByNameQueryHandlerAsync(DbContextOptions<AnimalContext> contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public override async Task<DogByNameQuery.Result> ExecuteAsync(DogByNameQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        await using var uow = new AnimalContext(_contextOptions);
        var repo = new DogRepositoryAsync(uow);
        var dogs = await repo.GetByNameAsync(query.DogName, cancellationToken);
        
        return new DogByNameQuery.Result(dogs.Select(d => new DogModel
        {
            Id = d.Id,
            Classification = d.Classification,
            Name = d.Name,
            Species = d.Species,
            Sound = d.Sound
        }).ToList());
    }
}