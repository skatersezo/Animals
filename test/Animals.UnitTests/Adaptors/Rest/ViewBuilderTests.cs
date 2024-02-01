using Animals.API.Builders;
using Animals.API.Builders.ViewBuilders;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Domain.Models;
using Animals.UnitTests.Support;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace Animals.UnitTests.Adaptors.Rest;

[TestFixture]
public class ViewBuilderTests
{
    private LinkBuilder _linkBuilder;
    
    [SetUp]
    public void Setup()
    {
        var linkGen = new FakeLinkGenerator();
        var fakeContext = new FakeHttpContext();
        _linkBuilder = new LinkBuilder(linkGen, fakeContext);
    }
    
    [Test]
    public void When_Building_An_AnimalView()
    {
        // Arrange
        var model = new AnimalModel()
        {
            Id = 1,
            Classification = "Mammal",
            Species = "Dog",
            Sound = "Woof woof"
        };
        
        var viewBuilder = new AnimalViewBuilder(_linkBuilder);
        
        // Act
        var view = viewBuilder.Build(model);
        
        // Assert
        view.Classification.ShouldBe(model.Classification);
        view.Species.ShouldBe(model.Species);
        view.Sound.ShouldBe(model.Sound);
        view.Links.ShouldBeEquivalentTo(
                new List<Link>
                {
                    _linkBuilder.Build(Rels.Self, HttpMethods.Get, RouteNames.GetAnimal, new { Id = model.Id}),
                    _linkBuilder.Build(Rels.ListAll, HttpMethods.Get, RouteNames.GetAnimals),
                    _linkBuilder.Build(Rels.Child, HttpMethods.Get, RouteNames.GetMammal, new { Id = model.Id})
                });
    }
    
    [Test]
    public void When_Building_An_AnimalsView()
    {
        // Arrange
        var animalsModel = new List<AnimalModel>
        {
            new() { Id = 1, Classification = "Mammal", Species = "Dog", Sound = "Woof woof" },
            new() { Id = 2, Classification = "Mammal", Species = "Cat", Sound = "Meow meow" },
            new() { Id = 3, Classification = "Bird", Species = "Pigeon", Sound = "Chirp chirp" }
        };

        var animalViewBuilder = new AnimalViewBuilder(_linkBuilder);
        var viewBuilder = new AnimalsViewBuilder(_linkBuilder, animalViewBuilder);
        
        // Act
        var view = viewBuilder.Build(animalsModel);
        
        // Assert
        view.AnimalViews.Count.ShouldBe(3);
        view.Links.ShouldBeEquivalentTo(
            new List<Link>
            {
                _linkBuilder.Build(Rels.Self, HttpMethods.Get, RouteNames.GetAnimals),
                _linkBuilder.Build(Rels.Child, HttpMethods.Get, RouteNames.GetMammals),
                _linkBuilder.Build(Rels.Child, HttpMethods.Get, RouteNames.GetBirds)
            });
    }
    
    [Test]
    public void When_Building_A_DogView()
    {
        // Arrange
        var model = new DogModel()
        {
            Id = 1,
            Name = "Sparky",
            Classification = "Mammal",
            Species = "Dog",
            Sound = "Woof woof"
        };
        
        var viewBuilder = new DogViewBuilder(_linkBuilder);
        
        // Act
        var view = viewBuilder.Build(model);
        
        // Assert
        view.Classification.ShouldBe(model.Classification);
        view.Name.ShouldBe(model.Name);
        view.Species.ShouldBe(model.Species);
        view.Sound.ShouldBe(model.Sound);
        view.Links.ShouldBeEquivalentTo(
            new List<Link>
            {
                _linkBuilder.Build(Rels.Self, HttpMethods.Get, RouteNames.GetDog, new { Id = model.Id }),
                _linkBuilder.Build(Rels.Parent, HttpMethods.Get, RouteNames.GetMammal, new { Id = model.Id }),
                _linkBuilder.Build(Rels.ListAll, HttpMethods.Get, RouteNames.GetDogs),
                _linkBuilder.Build(Rels.Add, HttpMethods.Post, RouteNames.AddDog),
                _linkBuilder.Build(Rels.Edit, HttpMethods.Put, RouteNames.EditDog, new { Id = model.Id }),
                _linkBuilder.Build(Rels.Delete, HttpMethods.Delete, RouteNames.DeleteDog, new { Id = model.Id })
            });
    }
}