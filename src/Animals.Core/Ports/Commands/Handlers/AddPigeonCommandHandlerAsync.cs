using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Repositories;
using Animals.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Paramore.Brighter;

namespace Animals.Core.Ports.Commands.Handlers;

public class AddPigeonCommandHandlerAsync : RequestHandlerAsync<AddPigeonCommand>
{
    private readonly DbContextOptions<AnimalContext> _contextOptions;
    public AddPigeonCommandHandlerAsync(DbContextOptions<AnimalContext> contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public override async Task<AddPigeonCommand> HandleAsync(AddPigeonCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        await using (var uow = new AnimalContext(_contextOptions))
        {
            var repo = new AnimalRepositoryAsync<Pigeon>(uow);
            var savedPigeon = await repo.AddAsync(new Pigeon(command.Colour), cancellationToken);
            command.PigeonId = savedPigeon.Id;
        }
        
        return await base.HandleAsync(command, cancellationToken); 
    }
}