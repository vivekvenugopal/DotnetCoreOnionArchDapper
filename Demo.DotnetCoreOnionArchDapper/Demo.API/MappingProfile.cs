using AutoMapper;
using Demo.DTO;

namespace Demo.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>(); // Define map for individual objects
        }
    }
}
