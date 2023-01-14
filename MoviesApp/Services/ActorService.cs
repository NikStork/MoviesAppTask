using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.Services.DTO;

namespace MoviesApp.Services;

public class ActorService : IActorService
{
    private readonly MoviesContext _context;
    private readonly IMapper _mapper;
    
    public ActorService(MoviesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public ActorsDto GetActor(int id)
    {
        return _mapper.Map<ActorsDto>(_context.Actors.FirstOrDefault(m => m.ActorId == id));
    }

    public IEnumerable<ActorsDto> GetAllActors()
    {
        return _mapper.Map<IEnumerable<Actors>, IEnumerable<ActorsDto>>(_context.Actors.ToList());
    }

    public ActorsDto UpdateActor(ActorsDto actorsDto)
    {
        if (actorsDto.ActorId == null)
        {
            return null;
        }
        
        try
        {
            var actors = _mapper.Map<Actors>(actorsDto);
            
            _context.Update(actors);
            _context.SaveChanges();
            
            return _mapper.Map<ActorsDto>(actors);
        }
        catch (DbUpdateException)
        {
            if (!ActorsExists((int) actorsDto.ActorId))
            {
                return null;
            }
            else
            {
                return null;
            }
        }
    }

    public ActorsDto AddActor(ActorsDto actorsDto)
    {
        var actors = _context.Add((object)_mapper.Map<Actors>(actorsDto)).Entity;
        _context.SaveChanges();
        return _mapper.Map<ActorsDto>(actors);
    }

    public ActorsDto DeleteActor(int id)
    {
        var actors = _context.Actors.Find(id);
        if (actors == null)
        {
            return null;
        }

        _context.Actors.Remove(actors);
        _context.SaveChanges();
        
        return _mapper.Map<ActorsDto>(actors);
    }
    
    private bool ActorsExists(int id)
    {
        return _context.Actors.Any(e => e.ActorId == id);
    }
}