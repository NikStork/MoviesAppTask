using System.Collections.Generic;
using MoviesApp.Services.DTO;

namespace MoviesApp.Services;

public interface IActorService
{
    ActorsDto GetActor(int id);
    IEnumerable<ActorsDto> GetAllActors();
    ActorsDto UpdateActor(ActorsDto actorsDto);
    ActorsDto AddActor(ActorsDto actorsDto);
    ActorsDto DeleteActor(int id);
}