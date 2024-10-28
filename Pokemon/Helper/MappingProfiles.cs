
using AutoMapper;
using Pokemon.Models;
using PokemonReviewer.Data.Dto;

namespace PokemonReviewer.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Pokemonn, PokemonDto>();
            CreateMap<PokemonDto, Pokemonn>();
            CreateMap<Catagory, CatagoryDto>();
            CreateMap<CatagoryDto, Catagory>();
            CreateMap<Country , CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto,Owner>();
            CreateMap<Review,ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
