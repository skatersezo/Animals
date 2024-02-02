using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Repositories;
using Animals.Core.Domain;
using Animals.Core.Domain.Models;
using Animals.Core.Ports.Exceptions;
using Microsoft.EntityFrameworkCore;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries.Handlers;

public class CatByIdQueryHandlerAsync : QueryHandlerAsync<CatByIdQuery, CatByIdQuery.Result>
{
    private readonly DbContextOptions<AnimalContext> _contextOptions;

    public CatByIdQueryHandlerAsync(DbContextOptions<AnimalContext> contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public override async Task<CatByIdQuery.Result> ExecuteAsync(CatByIdQuery query, CancellationToken cancellationToken = new())
    {
        await using var uow = new AnimalContext(_contextOptions);
        var repo = new AnimalRepositoryAsync<Cat>(uow);
        var cat = await repo.GetAsync(query.CatId, cancellationToken);

        if (cat is null)
            throw new CatNotFoundException(query.CatId);

        return new CatByIdQuery.Result(new CatModel
        {
            Id = cat.Id,
            Classification = cat.Classification,
            FavouriteToy = cat.FavouriteToy,
            Species = cat.Species,
            Sound = cat.Sound
        });
    }
}