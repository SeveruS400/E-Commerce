using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace araba_al_sat.Infastructe.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductDtoForInsertion, Products>();
            CreateMap<ProductDtoForUpdate, Products>().ReverseMap(); 
        }
    }
}
