using AutoMapper;
using DataAccessLayer.Entities;
using BusinessAccessLayer.Dto;

namespace PresentationLayer.Infrastucture.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pet, PetDto>();
            CreateMap<User, UserDto>();
        }
    }
}
