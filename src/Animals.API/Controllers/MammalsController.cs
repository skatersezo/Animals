using Animals.API.Controllers.Requests;
using Animals.Core.Ports.Commands;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;
using Paramore.Darker;

namespace Animals.API.Controllers;

[Route("animals/mammals")]
public class MammalsController : Controller
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IAmACommandProcessor _commandProcessor;

    public MammalsController(
        IQueryProcessor queryProcessor, 
        IAmACommandProcessor commandProcessor)
    {
        _queryProcessor = queryProcessor;
        _commandProcessor = commandProcessor;
    }
    
    [HttpPost("dogs", Name = "PostDog")]
    public async Task<IActionResult> AddDog([FromBody] AddDogRequest request)
    {
        await _commandProcessor.SendAsync(new AddDogCommand(request.Name));
        
        return Ok();
    }
}