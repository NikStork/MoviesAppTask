using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.Services.DTO;

public class ActorsDto
{
    public int? ActorId { get; set; }
        
    [Required]
    [ActorNameLength(4)]
    public string FirstName { get; set; }
    
    [Required]
    [ActorNameLength(4)]
    public string LastName { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
}