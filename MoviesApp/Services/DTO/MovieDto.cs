using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Services.DTO;

public class MovieDto
{
    public int? Id { get; set; }
        
    [Required]
    [StringLength(32,ErrorMessage = "Title length can't be more than 32.")]
    public string Title { get; set; }
        
        
    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
        
    [Required]
    public string Genre { get; set; }
        
    [Required]
    [Range(0, 999.99)]
    public decimal Price { get; set; }
}