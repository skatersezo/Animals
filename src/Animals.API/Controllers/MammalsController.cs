using System.Net;
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
    private readonly IViewBuilder<CatModel, CatView> _catViewBuilder;

    public MammalsController(
        IQueryProcessor queryProcessor, 
        IAmACommandProcessor commandProcessor, 
        IViewBuilder<DogModel, DogView> dogViewBuilder, 
        IViewBuilder<CatModel, CatView> catViewBuilder)
    {
        _queryProcessor = queryProcessor;
        _commandProcessor = commandProcessor;
        _dogViewBuilder = dogViewBuilder;
        _catViewBuilder = catViewBuilder;
    }
    
    [HttpGet(Name = RouteNames.GetMammals)]
    public async Task<IActionResult> GetMammals()
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpGet("{id:int}", Name = RouteNames.GetMammal)]
    public async Task<IActionResult> GetMammal([FromRoute] int id)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpPost("dogs", Name = RouteNames.AddDog)]
    public async Task<IActionResult> AddDog([FromBody] AddDogRequest request)
    {
        var command = new AddDogCommand(request.Name);
        await _commandProcessor.SendAsync(command);
        
        var result = await _queryProcessor.ExecuteAsync(new DogByIdQuery(command.DogId));
        var view = _dogViewBuilder.Build(result.DogModel);
        
        return CreatedAtRoute(RouteNames.GetDog, new { id = command.DogId }, view);
    }
    
    [HttpGet("dogs", Name = RouteNames.GetDogs)]
    public async Task<IActionResult> GetDogs()
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpGet("dogs/{id:int}", Name = RouteNames.GetDog)]
    public async Task<IActionResult> GetDog([FromRoute] int id)
    {
        var result = await _queryProcessor.ExecuteAsync(new DogByIdQuery(id));

        var view = _dogViewBuilder.Build(result.DogModel);
        
        return Ok(view);
    }
    
    [HttpPut("dogs/{id:int}", Name = RouteNames.EditDog)]
    public async Task<IActionResult> EditDog([FromRoute] int id, [FromBody] EditDogRequest request)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpDelete("dogs/{id:int}", Name = RouteNames.DeleteDog)]
    public async Task<IActionResult> DeleteDog([FromRoute] int id)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpDelete("dogs", Name = RouteNames.DeleteDogs)]
    public async Task<IActionResult> DeleteDogs()
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpPost("cats", Name = RouteNames.AddCat)]
    public async Task<IActionResult> AddCat([FromBody] AddCatRequest request)
    {
        var command = new AddCatCommand(request.FavouriteToy);
        await _commandProcessor.SendAsync(command);

        var result = await _queryProcessor.ExecuteAsync(new CatByIdQuery(command.CatId));
        var view = _catViewBuilder.Build(result.CatModel);
        
        return CreatedAtRoute(RouteNames.GetCat, new { id = command.CatId }, view);
    }
    
    [HttpGet("cats", Name = RouteNames.GetCats)]
    public async Task<IActionResult> GetCats()
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpGet("cats/{id:int}", Name = RouteNames.GetCat)]
    public async Task<IActionResult> GetCat([FromRoute] int id)
    {
        var result = await _queryProcessor.ExecuteAsync(new CatByIdQuery(id));
        var view = _catViewBuilder.Build(result.CatModel);

        return Ok(view);
    }
    
    [HttpPut("cats/{id:int}", Name = RouteNames.EditCat)]
    public async Task<IActionResult> EditCat([FromRoute] int id, [FromBody] EditCatRequest request)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpDelete("cats/{id:int}", Name = RouteNames.DeleteCat)]
    public async Task<IActionResult> DeleteCat([FromRoute] int id)
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
    
    [HttpDelete("cats", Name = RouteNames.DeleteCats)]
    public async Task<IActionResult> DeleteCats()
    {
        return StatusCode(StatusCodes.Status418ImATeapot);
    }
}