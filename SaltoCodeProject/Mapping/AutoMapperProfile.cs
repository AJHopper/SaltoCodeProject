using AutoMapper;
using SaltoCodeProject.Entities;
using SaltoCodeProject.Models;

namespace SaltoCodeProject.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserUpdateModel, User>();
            CreateMap<UserCreateModel, User>();
            CreateMap<LockCreateModel, Lock>();
        }
    }
}
