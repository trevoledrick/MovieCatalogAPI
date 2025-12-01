using System.Linq;
using MovieholicAPI.Models.Domain;
using MovieholicAPI.Models.DTO.Franchise;
using AutoMapper;

namespace MovieholicAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            // Franchise -> FranchiseReadDTO
            CreateMap<Franchise, FranchiseReadDTO>()
                .ReverseMap();

            // FranchiseCreateDTO -> Franchise
            CreateMap<FranchiseCreateDTO, Franchise>()
                .ReverseMap();

            // FranchiseEditDTO -> Franchise
            CreateMap<FranchiseEditDTO, Franchise>()
                .ReverseMap();

            // Franchise -> FranchiseMoviesReadDTO
            CreateMap<Franchise, FranchiseMoviesReadDTO>()
                .ForMember(frdto => frdto.Movies,
                opt => opt.MapFrom(b => b.Movies
                .Select(b => b.MovieId).ToArray()))
                .ReverseMap();
        }
    }
}