using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Repositories;
using Animals.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries.Handlers;

public class AnimalByIdQueryHandlerAsync : QueryHandlerAsync<AnimalByIdQuery, AnimalByIdQuery.Result<Animal>>
{
    private readonly DbContextOptions<AnimalContext> _contextOptions;

    public AnimalByIdQueryHandlerAsync(DbContextOptions<AnimalContext> contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public override async Task<AnimalByIdQuery.Result<Animal>> ExecuteAsync(AnimalByIdQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        await using var uow = new AnimalContext(_contextOptions);
        var repo = new AnimalRepositoryAsync<Animal>(uow);

        var animal = await repo.GetAsync(query.AnimalId, cancellationToken);
        
        return new AnimalByIdQuery.Result<Animal>(animal);
    }
}