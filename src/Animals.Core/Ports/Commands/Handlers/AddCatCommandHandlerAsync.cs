using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Repositories;
using Animals.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Paramore.Brighter;

namespace Animals.Core.Ports.Commands.Handlers;

public class AddCatCommandHandlerAsync : RequestHandlerAsync<AddCatCommand>
{
    private readonly DbContextOptions<AnimalContext> _contextOptions;
    public AddCatCommandHandlerAsync(DbContextOptions<AnimalContext> contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public override async Task<AddCatCommand> HandleAsync(AddCatCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        await using (var uow = new AnimalContext(_contextOptions))
        {
            var repo = new AnimalRepositoryAsync<Cat>(uow);
            var savedCat = await repo.AddAsync(new Cat(command.FavouriteToy), cancellationToken);
            command.CatId = savedCat.Id;
        }
        
        return await base.HandleAsync(command, cancellationToken);
    }
}