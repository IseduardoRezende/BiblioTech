using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Users;

namespace BiblioTechDomain.Services.IService
{
    public interface IUserService : IBaseService<CreateUserSignUpViewModel, UpdateUserViewModel, ReadUserViewModel, User>
    {    
        Task<ReadConnectedUserViewModel> SignInAsync(CreateUserSignInViewModel readSignInViewModel);

        Task<ReadUserViewModel> ChangePasswordAsync(UpdateUserPasswordViewModel updateUserPasswordViewModel);                 
    }
}
