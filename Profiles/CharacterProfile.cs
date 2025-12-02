using MovieCatalogAPI.Models.Domain;
using MovieCatalogAPI.Models.DTO.Character;
using AutoMapper;

namespace MovieCatalogAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            // Character <-> CharacterReadDTO
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(crdto => crdto.Id,
                opt => opt.MapFrom(b => b.CharacterId))
                .ReverseMap();

            // Character <-> CharacterCreateDTO
            CreateMap<Character, CharacterCreateDTO>()
            .ReverseMap();

            // Character <-> CharacterEditDTO
            CreateMap<Character, CharacterEditDTO>()
                .ForMember(crdto => crdto.Id,
                opt => opt.MapFrom(b => b.CharacterId))
                .ReverseMap();
        }
    }
}