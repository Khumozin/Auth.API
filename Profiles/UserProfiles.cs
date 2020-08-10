using Auth.API.Dtos;
using Auth.API.Models;
using AutoMapper;

namespace Auth.API.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            // Source -> Target
            // CREATE
            CreateMap<UserCreateDto, User>();
            // READ
            CreateMap<User, UserReadDto>();
            // UPDATE
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }
}