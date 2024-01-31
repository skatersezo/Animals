using Animals.Core.Adaptors.Db;
using Animals.Core.Adaptors.Repositories;
using Animals.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Paramore.Brighter;

namespace Animals.Core.Ports.Commands.Handlers;

public class AddDogCommandHandlerAsync : RequestHandlerAsync<AddDogCommand>
{
    private readonly DbContextOptions<AnimalContext> _contextOptions;

    public AddDogCommandHandlerAsync(DbContextOptions<AnimalContext> contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public override async Task<AddDogCommand> HandleAsync(AddDogCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        await using (var uow = new AnimalContext(_contextOptions))
        {
            var repo = new AnimalRepositoryAsync<Dog>(uow);
            var savedDog = await repo.AddAsync(new Dog(command.Name), cancellationToken);
            command.DogId = savedDog.Id;
        }
        
        return await base.HandleAsync(command, cancellationToken);
    }
}