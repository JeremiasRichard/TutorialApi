using AutoMapper;
using PokemonReviewApp.DTOS;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.AutomapperProfile
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Country,CountryDTO>();
        }
    }
}
