using AutoMapper;
using NetApi.Models;
using NetApi.Entities;

namespace NetApi.Mappings
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<AddUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
        }
    }
}
