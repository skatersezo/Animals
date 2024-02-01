using Animals.API.Controllers.Requests;
using Animals.Core.Adaptors.Rest;
using Animals.Core.Ports.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Paramore.Brighter;
using Paramore.Darker;

namespace Animals.API.Controllers;

[Route("animals/birds")]
public class BirdsController : Controller
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IAmACommandProcessor _commandProcessor;
    
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
        await _commandProcessor.SendAsync(new AddPigeonCommand(request.Colour));
        
        return Ok();
    }
    
    [HttpGet("pigeons", Name = RouteNames.GetPigeons)]
    public async Task<IActionResult> GetPigeons()
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpGet("pigeons/{id:int}", Name = RouteNames.GetPigeon)]
    public async Task<IActionResult> GetPigeon([FromRoute] int id)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
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