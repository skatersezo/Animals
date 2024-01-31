using Animals.Core.Ports.Queries;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;
using Paramore.Darker;

namespace Animals.API.Controllers;

[Route("animals")]
public class AnimalsController : Controller
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IAmACommandProcessor _commandProcessor;


    public AnimalsController(
        IQueryProcessor queryProcessor, 
        IAmACommandProcessor commandProcessor)
    {
        _queryProcessor = queryProcessor;
        _commandProcessor = commandProcessor;
    }

    [HttpGet(Name = "GetAnimals")]
    public async Task<IActionResult> GetAnimals()
    {
        var result = await _queryProcessor.ExecuteAsync(new AnimalsQuery());
        
        return Ok(result);
    }
    
    [HttpGet("{id:int}", Name = "GetAnimal")]
    public async Task<IActionResult> GetAnimal([FromRoute] int id)
    {
        var result = await _queryProcessor.ExecuteAsync(new AnimalByIdQuery(id));
        
        return Ok(result);
    }
}