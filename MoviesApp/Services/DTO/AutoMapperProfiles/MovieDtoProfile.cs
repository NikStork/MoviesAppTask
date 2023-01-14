using AutoMapper;
using MoviesApp.Models;

namespace MoviesApp.Services.DTO.AutoMapperProfiles;

public class MovieDtoProfile : Profile
{
    public MovieDtoProfile()
    {
        CreateMap<Movie, MovieDto>().ReverseMap();
    }
}