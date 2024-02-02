using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Repositories;
using Animals.Core.Domain;
using Animals.Core.Domain.Models;
using Animals.Core.Ports.Exceptions;
using Microsoft.EntityFrameworkCore;
using Paramore.Darker;

namespace Animals.Core.Ports.Queries.Handlers;

public class PigeonByIdQueryHandlerAsyc : QueryHandlerAsync<PigeonByIdQuery, PigeonByIdQuery.Result>
{
    private readonly DbContextOptions<AnimalContext> _contextOptions;

    public PigeonByIdQueryHandlerAsyc(DbContextOptions<AnimalContext> contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public override async Task<PigeonByIdQuery.Result> ExecuteAsync(PigeonByIdQuery query, CancellationToken cancellationToken = new())
    {
        await using var uow = new AnimalContext(_contextOptions);
        var repo = new AnimalRepositoryAsync<Pigeon>(uow);
        var pigeon = await repo.GetAsync(query.PigeonId, cancellationToken);

        if (pigeon is null)
            throw new PigeonNotFoundException(query.PigeonId);

        return new PigeonByIdQuery.Result(new PigeonModel()
        {
            Id = pigeon.Id,
            Classification = pigeon.Classification,
            Colour = pigeon.Colour,
            Species = pigeon.Species,
            Sound = pigeon.Sound
        });
    }
}