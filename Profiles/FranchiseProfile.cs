using System.Linq;
using MovieCatalogAPI.Models.Domain;
using MovieCatalogAPI.Models.DTO.Franchise;
using AutoMapper;

namespace MovieCatalogAPI.Profiles
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