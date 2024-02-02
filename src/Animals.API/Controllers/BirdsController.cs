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

[Route("animals/birds")]
public class BirdsController : Controller
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IAmACommandProcessor _commandProcessor;
    private readonly IViewBuilder<PigeonModel, PigeonView> _pigeonViewBuilder;

    public BirdsController(
        IQueryProcessor queryProcessor, 
        IAmACommandProcessor commandProcessor, 
        IViewBuilder<PigeonModel, PigeonView> pigeonViewBuilder)
    {
        _queryProcessor = queryProcessor;
        _commandProcessor = commandProcessor;
        _pigeonViewBuilder = pigeonViewBuilder;
    }

    [HttpGet(Name = RouteNames.GetBirds)]
    public async Task<IActionResult> GetBirds()
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpGet("{id:int}", Name = RouteNames.GetBird)]
    public async Task<IActionResult> GetBird([FromRoute] int id)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpPost("pigeons", Name = RouteNames.AddPigeon)]
    public async Task<IActionResult> AddPigeon([FromBody] AddPigeonRequest request)
    {
        var command = new AddPigeonCommand(request.Colour);
        await _commandProcessor.SendAsync(command);

        var result = await _queryProcessor.ExecuteAsync(new PigeonByIdQuery(command.PigeonId));
        var view = _pigeonViewBuilder.Build(result.PigeonModel);
        
        return CreatedAtRoute(RouteNames.GetPigeon, new { id = command.PigeonId }, view);
    }
    
    [HttpGet("pigeons", Name = RouteNames.GetPigeons)]
    public async Task<IActionResult> GetPigeons()
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpGet("pigeons/{id:int}", Name = RouteNames.GetPigeon)]
    public async Task<IActionResult> GetPigeon([FromRoute] int id)
    {
        var result = await _queryProcessor.ExecuteAsync(new PigeonByIdQuery(id));
        var view = _pigeonViewBuilder.Build(result.PigeonModel);

        return Ok(view);
    }
    
    [HttpPut("pigeons/{id:int}", Name = RouteNames.EditPigeon)]
    public async Task<IActionResult> EditPigeon([FromRoute] int id, [FromBody] EditPigeonRequest request)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpDelete("pigeons/{id:int}", Name = RouteNames.DeletePigeon)]
    public async Task<IActionResult> DeletePigeon([FromRoute] int id)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpDelete("pigeons", Name = RouteNames.DeletePigeons)]
    public async Task<IActionResult> DeletePigeons()
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
}