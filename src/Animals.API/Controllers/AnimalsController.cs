using Animals.API.Builders;
using Animals.API.Views;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Domain.Models;
using Animals.Core.Ports.Queries;
using Microsoft.AspNetCore.Mvc;
using Paramore.Darker;

namespace Animals.API.Controllers;

[Route("animals")]
public class AnimalsController : Controller
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IViewBuilder<AnimalModel, AnimalView> _animalViewBuilder;
    private readonly IViewBuilder<List<AnimalModel>, AnimalsView> _animalsViewBuilder;

    public AnimalsController(
        IQueryProcessor queryProcessor,
        IViewBuilder<AnimalModel, AnimalView> animalViewBuilder, 
        IViewBuilder<List<AnimalModel>, AnimalsView> animalsViewBuilder)
    {
        _queryProcessor = queryProcessor;
        _animalViewBuilder = animalViewBuilder;
        _animalsViewBuilder = animalsViewBuilder;
    }

    [HttpGet(Name = RouteNames.GetAnimals)]
    public async Task<IActionResult> GetAnimals()
    {
        var result = await _queryProcessor.ExecuteAsync(new AnimalsQuery());

        var view = _animalsViewBuilder.Build(result.AnimalModels);
        
        return Ok(view);
    }
    
    [HttpGet("{id:int}", Name = RouteNames.GetAnimal)]
    public async Task<IActionResult> GetAnimal([FromRoute] int id)
    {
        var result = await _queryProcessor.ExecuteAsync(new AnimalByIdQuery(id));

        var view = _animalViewBuilder.Build(result.AnimalModel);
        
        return Ok(view);
    }
}