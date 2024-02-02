using Animals.Core.Adaptors.Db;
using Animals.Core.Ports.Commands;
using Animals.Core.Ports.Commands.Handlers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Animals.UnitTests.Ports.CommandHandlers;

[TestFixture]
public class AddCatCommandHandlerTests
{
    private AnimalContext _context;
    private DbContextOptions<AnimalContext> _options;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<AnimalContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AnimalContext(_options);
    }
    
    [TearDown]
    public void Teardown()
    {
        _context.Dispose();
    }
    
    [Test]
    public async Task When_Adding_A_Cat()
    {
        // Arrange
        var command = new AddCatCommand("wool ball");
        var handler = new AddCatCommandHandlerAsync(_options);
        
        // Act
        await handler.HandleAsync(command);
        
        // Arrange
        _context.Cats.Count().ShouldBe(1);
        _context.Cats.SingleAsync().Result.FavouriteToy.ShouldBe("wool ball");
        _context.Cats.SingleAsync().Result.Sound.ShouldBe("meow meow");
    }
}