using Animals.Core.Adaptors.Db;
using Animals.Core.Ports.Commands;
using Animals.Core.Ports.Commands.Handlers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Animals.UnitTests.Ports.CommandHandlers;

[TestFixture]
public class AddDogCommandHandlerTests
{
    private AnimalContext _context;
    private DbContextOptions<AnimalContext> _options;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<AnimalContext>()
            .UseInMemoryDatabase("CommandUnitTestsDb")
            .Options;

        _context = new AnimalContext(_options);
    }
    
    [Test]
    public async Task When_Adding_A_Dog_As_Authorised_User()
    {
        // Arrange
        var command = new AddDogCommand("Balto");
        var handler = new AddDogCommandHandlerAsync(_options);
        
        // Act
        await handler.HandleAsync(command);
        
        // Arrange
        _context.Dogs.Count().ShouldBe(1);
        _context.Dogs.SingleAsync().Result.Name.ShouldBe("Balto");
        _context.Dogs.SingleAsync().Result.Sound.ShouldBe("woof woof");
    }
}