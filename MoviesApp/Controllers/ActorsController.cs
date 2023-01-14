using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Filters;
using MoviesApp.Models;
using MoviesApp.ViewModels;
using MoviesApp.ViewModels.ActorsViewModels;

namespace MoviesApp.Controllers;

public class ActorsController : Controller
{
    private readonly MoviesContext dmContext;
    private readonly ILogger<HomeController> dmLogger;
    private readonly IMapper dmMapper;

    public ActorsController(MoviesContext moviesContext, ILogger<HomeController> logger, IMapper mapper)
    {
        dmContext = moviesContext;
        dmLogger = logger;
        dmMapper = mapper;
    }
    
    // GET: Actors
    [HttpGet]
    public IActionResult Index()
    {
        return View(dmMapper.Map<IEnumerable<Actors>, IEnumerable<ActorViewModel>>(dmContext.Actors.ToList()));
    }

    // GET: Actors/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: Actors/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActorAcceptableAge]
    public IActionResult Create([Bind("FirstName,LastName,BirthDate")] InputActorViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            dmContext.Add(dmMapper.Map<Actors>(inputModel));
            dmContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        return View(inputModel);
    }
    
    [HttpGet]
    // GET: Actors/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var editModel = dmMapper.Map<EditActorViewModel>(dmContext.Actors.FirstOrDefault(m => m.ActorId == id));
            
        if (editModel == null)
        {
            return NotFound();
        }
            
        return View(editModel);
    }
    
    // POST: Actors/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActorAcceptableAge]
    public IActionResult Edit(int id, [Bind("FirstName,LastName,BirthDate")] EditActorViewModel editModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var actor = dmMapper.Map<Actors>(editModel);
                actor.ActorId = id;
                    
                dmContext.Update(actor);
                dmContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (!ActorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(editModel);
    }
    
    [HttpGet]
    // GET: Actors/Delete/5
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var deleteModel = dmMapper.Map<DeleteActorViewModel>(dmContext.Actors.FirstOrDefault(m => m.ActorId == id));
            
        if (deleteModel == null)
        {
            return NotFound();
        }

        return View(deleteModel);
    }
    
    // GET: Actors/Details/5
    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var viewModel = dmMapper.Map<ActorViewModel>(dmContext.Actors.FirstOrDefault(a => a.ActorId == id));

        if (viewModel == null)
        {
            return NotFound();
        }

        return View(viewModel);
    }
        
    // POST: Actors/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var actor = dmContext.Actors.FirstOrDefault(a => a.ActorId == id);
        dmContext.Actors.Remove(actor);
        dmContext.SaveChanges();
        dmLogger.LogError($"Actor with id {actor.ActorId} has been deleted!");
        return RedirectToAction(nameof(Index));
    }
    
    private bool ActorExists(int id)
    {
        return dmContext.Actors.Any(e => e.ActorId == id);
    }
}