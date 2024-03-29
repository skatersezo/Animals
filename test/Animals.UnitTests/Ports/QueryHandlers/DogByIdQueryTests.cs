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
public class DogByIdQueryTests
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
    public async Task When_Retrieving_A_Dog_By_Id()
    {
        // Arrange
        var dog = new Dog("Pancho");
        _context.Dogs.Add(dog);
        await _context.SaveChangesAsync();

        var handler = new DogByIdQueryHandlerAsync(_options);
        
        // Act
        var result = await handler.ExecuteAsync(new DogByIdQuery(dog.Id));
        
        // Arrange
        result.DogModel.Id.ShouldBe(dog.Id);
        result.DogModel.Classification.ShouldBe(dog.Classification);
        result.DogModel.Species.ShouldBe(dog.Species);
        result.DogModel.Name.ShouldBe(dog.Name);
        result.DogModel.Sound.ShouldBe(dog.Sound);
    }

    [Test]
    public async Task When_Retrieving_Dogs_By_Name()
    {
        // Arrange
        var dogA = new Dog("Bruno");
        var dogB = new Dog("Bruno");
        _context.Dogs.Add(dogA);
        _context.Dogs.Add(dogB);
        await _context.SaveChangesAsync();

        var handler = new DogByNameQueryHandlerAsync(_options);
        
        // Act
        var result = await handler.ExecuteAsync(new DogByNameQuery(dogA.Name));
        
        // Arrange
        result.DogModels.Count.ShouldBe(2);
        result.DogModels.ForEach(d => d.Name.ShouldBe("Bruno"));
    }
    
    [Test]
    public async Task Whe_Retrieving_Non_Existing_Dog()
    {
        // Arrange
        var dog = new Dog("Pancho");
        _context.Dogs.Add(dog);
        await _context.SaveChangesAsync();
        var id = 999;

        var handler = new DogByIdQueryHandlerAsync(_options);
        
        // Act
        Func<Task<DogByIdQuery.Result>> result = async () => await handler.ExecuteAsync(new DogByIdQuery(id));
        
        // Arrange
        Should.Throw<DogNotFoundException>(result.Invoke(), $"Dog with id {id} not found");
    }
}