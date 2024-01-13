using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;

namespace MagicVilla_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>().ReverseMap();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();

            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
            

            CreateMap<VillaNumber, VillaDto>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();
        }
    }
}
