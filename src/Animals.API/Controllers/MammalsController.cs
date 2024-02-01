using Animals.API.Builders;
using Animals.API.Controllers.Requests;
using Animals.API.Views;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Domain.Models;
using Animals.Core.Ports.Commands;
using Animals.Core.Ports.Queries;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;
using Paramore.Darker;

namespace Animals.API.Controllers;

[Route("animals/mammals")]
public class MammalsController : Controller
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IAmACommandProcessor _commandProcessor;
    private readonly IViewBuilder<DogModel, DogView> _dogViewBuilder;

    public MammalsController(
        IQueryProcessor queryProcessor, 
        IAmACommandProcessor commandProcessor, IViewBuilder<DogModel, DogView> dogViewBuilder)
    {
        _queryProcessor = queryProcessor;
        _commandProcessor = commandProcessor;
        _dogViewBuilder = dogViewBuilder;
    }
    
    [HttpPost("dogs", Name = RouteNames.AddDog)]
    public async Task<IActionResult> AddDog([FromBody] AddDogRequest request)
    {
        await _commandProcessor.SendAsync(new AddDogCommand(request.Name));
        
        return Ok();
    }
    
    [HttpPost("dogs/{id:int}", Name = RouteNames.GetDog)]
    public async Task<IActionResult> GetDog([FromRoute] int id)
    {
        var result = await _queryProcessor.ExecuteAsync(new DogQuery(id));

        var view = _dogViewBuilder.Build(result.DogModel);
        
        return Ok(view);
    }
}