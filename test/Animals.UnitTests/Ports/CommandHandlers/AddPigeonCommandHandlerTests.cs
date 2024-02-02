using Animals.Core.Adaptors.Db;
using Animals.Core.Ports.Commands;
using Animals.Core.Ports.Commands.Handlers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Animals.UnitTests.Ports.CommandHandlers;

[TestFixture]
public class AddPigeonCommandHandlerTests
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
    public async Task When_Adding_A_Pigeon()
    {
        // Arrange
        var command = new AddPigeonCommand("blue");
        var handler = new AddPigeonCommandHandlerAsync(_options);
        
        // Act
        await handler.HandleAsync(command);
        
        // Arrange
        _context.Pigeons.Count().ShouldBe(1);
        _context.Pigeons.SingleAsync().Result.Colour.ShouldBe("blue");
        _context.Pigeons.SingleAsync().Result.Sound.ShouldBe("coo coo");
    }
}