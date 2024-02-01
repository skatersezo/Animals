using Animals.Core.Adaptors.Rest;
using Microsoft.AspNetCore.Mvc;
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
        return StatusCode(StatusCodes.Status418ImATeapot);
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
    public async Task<IActionResult> EditDog([FromRoute] int id, [FromBody] EditPigeonRequest request)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpDelete("pigeons/{id:int}", Name = RouteNames.DeletePigeon)]
    public async Task<IActionResult> DeleteDog([FromRoute] int id)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpDelete("pigeons", Name = RouteNames.DeletePigeons)]
    public async Task<IActionResult> DeleteDogs()
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
}

public class EditPigeonRequest
{
}

public class AddPigeonRequest
{
}