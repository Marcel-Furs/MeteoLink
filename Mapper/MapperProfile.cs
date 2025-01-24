using AutoMapper;
using MeteoLink.Data.Models;
using MeteoLink.Dto;
using Microsoft.AspNetCore.Identity;

namespace MeteoLink.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserModel>();
        }
    }
}
