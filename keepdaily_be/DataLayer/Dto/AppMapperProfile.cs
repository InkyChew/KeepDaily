using AutoMapper;
using DomainLayer.Models;

namespace DomainLayer.Dto
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<UserDto, User>();
        }
    }
}
