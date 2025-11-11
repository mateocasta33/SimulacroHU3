using AutoMapper;
using management.Domain.Entities;
using management.Application.DTOs;

namespace management.Application.DTOs
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductCreateDto>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductUpdateDto>();

            CreateMap<User, UserDto>(); 
            CreateMap<UserUpdateDto, User>();
            
            // Mapeo de registro -> usuario (IMPORTANTE)
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));

            CreateMap<User, AuthResponseDto>();
        }
    }
}