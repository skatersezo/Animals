using Animals.Core.Adaptors.Db;
using Animals.Core.Domain;
using Animals.Core.Ports.Exceptions;
using Animals.Core.Ports.Queries;
using Animals.Core.Ports.Queries.Handlers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Animals.UnitTests.Ports.QueryHandlers;

[TestFixture]
public class AnimalQueryTests
{
    private AnimalContext _context;
    private DbContextOptions<AnimalContext> _options;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<AnimalContext>()
            .UseInMemoryDatabase("QueryUnitTestsDb")
            .Options;

        _context = new AnimalContext(_options);
    }
    
    [Test]
    public async Task When_Retrieving_An_Animal_By_Id_As_Authorised_User()
    {
        // Arrange
        var cat = new Cat("ball");
        _context.Cats.Add(cat);
        await _context.SaveChangesAsync();

        var handler = new AnimalByIdQueryHandlerAsync(_options);
        
        // Act
        var result = await handler.ExecuteAsync(new AnimalByIdQuery(cat.Id));
        
        // Arrange
        result.AnimalModel.Id.ShouldBe(cat.Id);
        result.AnimalModel.Classification.ShouldBe(cat.Classification);
        result.AnimalModel.Species.ShouldBe(cat.Species);
        result.AnimalModel.Sound.ShouldBe(cat.Sound);
    }
    
    [Test]
    public async Task When_Retrieving_All_Animals_As_Authorised_User()
    {
        // Arrange
        var cat = new Cat("feather");
        var dog = new Dog("Mushu");
        var pigeon = new Pigeon("blue");
        _context.Cats.Add(cat);
        _context.Dogs.Add(dog);
        _context.Pigeons.Add(pigeon);
        await _context.SaveChangesAsync();

        var handler = new AnimalsQueryHandlerAsync(_options);
        
        // Act
        var result = await handler.ExecuteAsync(new AnimalsQuery());
        
        // Arrange
        result.AnimalModels.Count.ShouldBe(3);
        result.AnimalModels.ShouldContain(a => a.Id == cat.Id);
        result.AnimalModels.ShouldContain(a => a.Id == dog.Id);
        result.AnimalModels.ShouldContain(a => a.Id == pigeon.Id);
    }
    
    [Test]
    public async Task When_Retrieving_A_Non_Existing_Animal_As_Authorised_User()
    {
        // Arrange
        var cat = new Cat("ball");
        _context.Cats.Add(cat);
        await _context.SaveChangesAsync();
        var id = 789;

        var handler = new AnimalByIdQueryHandlerAsync(_options);
        
        // Act
        Func<Task<AnimalByIdQuery.Result<Animal>>> result = async () => await handler.ExecuteAsync(new AnimalByIdQuery(id));
        
        // Arrange
        Should.Throw<AnimalNotFoundException>(result.Invoke(), $"Animal with id {id} not found");
    }
    
    [Test]
    public async Task When_Retrieving_An_Animal_By_Id_As_Unauthorised_User()
    {
        throw new NotImplementedException();
    }
    
    [Test]
    public async Task When_Retrieving_Animals_As_Unauthorised_User()
    {
        throw new NotImplementedException();
    }
}