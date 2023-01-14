using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.Services;
using MoviesApp.Services.DTO;
using MoviesApp.ViewModels.ActorsViewModels;

namespace MoviesApp.Controllers;

[Route("api/actors")]
[ApiController]
public class ActorsApiController : ControllerBase
{
    private readonly IActorService _service;

    public ActorsApiController(IActorService service)
    {
        _service = service;
    }

    [HttpGet] // GET: /api/actors
    [ProducesResponseType(200, Type = typeof(IEnumerable<ActorsDto>))]  
    [ProducesResponseType(404)]
    public ActionResult<IEnumerable<ActorsDto>> GetActors()
    {
        return Ok(_service.GetAllActors());
    }
    
    [HttpGet("{id}")] // GET: /api/actors/5
    [ProducesResponseType(200, Type = typeof(ActorsDto))]  
    [ProducesResponseType(404)]
    public IActionResult GetById(int id)
    {
        var actors = _service.GetActor(id);
        if (actors == null) return NotFound();  
        
        return Ok(actors);
    }
    
    [HttpPost] // POST: api/actors
    public ActionResult<InputActorViewModel> PostActor(ActorsDto actorsDto)
    {

        var actors = _service.AddActor(actorsDto);

        return CreatedAtAction("GetById", new { id = actors.ActorId }, actors);
    }
    
    [HttpPut("{id}")] // PUT: api/actors/5
    public IActionResult UpdateActor(int id, ActorsDto actorsDto)
    {
        var actors = _service.UpdateActor(actorsDto);

        if (actors == null) return NotFound();
        
        return Ok(actors);
    }
    
    [HttpDelete("{id}")] // DELETE: api/actors/5
    public ActionResult<ActorsDto> DeleteActor(int id)
    {
        var actors = _service.DeleteActor(id);
        if (actors == null) return NotFound();  
        
        return Ok(actors);
    }
}