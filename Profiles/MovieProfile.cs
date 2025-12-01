using System.Linq;
using MovieholicAPI.Models.Domain;
using MovieholicAPI.Models.DTO.Movie;
using AutoMapper;

namespace MovieholicAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            // Movie -> MovieReadDTO
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(mrdto => mrdto.Id,
                opt => opt.MapFrom(b => b.MovieId));

            // MovieCreateDTO -> Movie
            CreateMap<MovieCreateDTO, Movie>()
                .ReverseMap();

            // MovieEditDTO -> Movie
            CreateMap<MovieEditDTO, Movie>()
                .ForMember(medto => medto.MovieId,
                opt => opt.MapFrom(b => b.Id))
                .ReverseMap();

            // Movie -> MovieCharactersReadDTO
            CreateMap<Movie, MovieCharactersReadDTO>()
                .ForMember(mrdto => mrdto.Id,
                opt => opt.MapFrom(b => b.MovieId))
                .ForMember(mrdto => mrdto.Characters,
                opt => opt.MapFrom(b => b.Characters
                .Select(b => b.CharacterId).ToArray()));
        }
    }
}