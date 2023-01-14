using AutoMapper;
using MoviesApp.Models;

namespace MoviesApp.Services.DTO.AutoMapperProfiles;

public class ActorsDtoProfile : Profile
{
    public ActorsDtoProfile()
    {
        CreateMap<Actors, ActorsDto>().ReverseMap();
    }
}