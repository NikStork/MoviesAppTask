using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.Services;
using MoviesApp.Services.DTO;
using MoviesApp.ViewModels.MoviesViewModels;


namespace MoviesApp.Controllers;

[Route("api/movies")]
[ApiController]
public class MoviesApiController : ControllerBase
{
    private readonly IMovieService _service;

    public MoviesApiController(IMovieService service)
    {
        _service = service;
    }

    [HttpGet] // GET: /api/movies
    [ProducesResponseType(200, Type = typeof(IEnumerable<MovieDto>))]  
    [ProducesResponseType(404)]
    public ActionResult<IEnumerable<MovieDto>> GetMovies()
    {
        var movies = _service.GetAllMovies();
        
        return Ok(movies);
    }
    
    [HttpGet("{id}")] // GET: /api/movies/5
    [ProducesResponseType(200, Type = typeof(MovieDto))]  
    [ProducesResponseType(404)]
    public IActionResult GetById(int id)
    {
        var movie = _service.GetMovie(id);
        
        if (movie == null) return NotFound();  
        
        return Ok(movie);
    }
    
    [HttpPost] // POST: api/movies
    public ActionResult<MovieDto> PostMovie(MovieDto movieDto)
    {
        var movie = _service.AddMovie(movieDto);

        return CreatedAtAction("GetById", new { id = movie.Id }, movie);
    }
    
    [HttpPut("{id}")] // PUT: api/movies/5
    public IActionResult UpdateMovie(int id, MovieDto movieDto)
    {
        var movie = _service.UpdateMovie(movieDto);
        movie.Id = id;

        return Ok(movie);
    }
    
    [HttpDelete("{id}")] // DELETE: api/movie/5
    public ActionResult<DeleteMovieViewModel> DeleteMovie(int id)
    {
        var movie = _service.DeleteMovie(id);
        
        if (movie == null) return NotFound();  
        
        return Ok(movie);
    }
}