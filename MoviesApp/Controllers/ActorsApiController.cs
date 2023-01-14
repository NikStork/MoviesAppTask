using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.ViewModels.ActorsViewModels;

namespace MoviesApp.Controllers;

[Route("api/actors")]
[ApiController]
public class ActorsApiController : ControllerBase
{
    private readonly MoviesContext _context;
    private readonly IMapper _mapper;

    public ActorsApiController(MoviesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet] // GET: /api/actors
    [ProducesResponseType(200, Type = typeof(IEnumerable<ActorViewModel>))]  
    [ProducesResponseType(404)]
    public ActionResult<IEnumerable<ActorViewModel>> GetActors()
    {
        var actors = _mapper.Map<IEnumerable<Actors>, IEnumerable<ActorViewModel>>(_context.Actors.ToList());
        return Ok(actors);
    }
    
    [HttpGet("{id}")] // GET: /api/actors/5
    [ProducesResponseType(200, Type = typeof(ActorViewModel))]  
    [ProducesResponseType(404)]
    public IActionResult GetById(int id)
    {
        var actors = _mapper.Map<ActorViewModel>(_context.Actors.FirstOrDefault(m => m.ActorId == id));
        if (actors == null) return NotFound();  
        return Ok(actors);
    }
    
    [HttpPost] // POST: api/actors
    public ActionResult<InputActorViewModel> PostActor(InputActorViewModel inputModel)
    {
        
        var actors = _context.Add(_mapper.Map<Actors>(inputModel)).Entity;
        _context.SaveChanges();

        return CreatedAtAction("GetById", new { id = actors.ActorId }, _mapper.Map<InputActorViewModel>(inputModel));
    }
    
    [HttpPut("{id}")] // PUT: api/actors/5
    public IActionResult UpdateActor(int id, EditActorViewModel editModel)
    {
        try
        {
            var actors = _mapper.Map<Actors>(editModel);
            actors.ActorId = id;
            
            _context.Update(actors);
            _context.SaveChanges();
            
            return Ok(_mapper.Map<EditActorViewModel>(actors));
        }
        catch (DbUpdateException)
        {
            if (!ActorsExists(id))
            {
                return BadRequest();
            }
            else
            {
                throw;
            }
        }
    }
    
    [HttpDelete("{id}")] // DELETE: api/actors/5
    public ActionResult<DeleteActorViewModel> DeleteActor(int id)
    {
        var actors = _context.Actors.Find(id);
        if (actors == null) return NotFound();  
        _context.Actors.Remove(actors);
        _context.SaveChanges();
        return Ok(_mapper.Map<DeleteActorViewModel>(actors));
    }

    private bool ActorsExists(int id)
    {
        return _context.Actors.Any(e => e.ActorId == id);
    }
}