using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.ViewModels.ActorsViewModels;

public class InputActorViewModel
{
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