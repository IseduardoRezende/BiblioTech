using AutoMapper;
using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Users;

namespace BiblioTechDomain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserSignUpViewModel, User>();
            CreateMap<UpdateUserViewModel, User>();
            CreateMap<ReadUserViewModel, ReadConnectedUserViewModel>();
            CreateMap<User, ReadUserViewModel>();            
        }
    }
}
