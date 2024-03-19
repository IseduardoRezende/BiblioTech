using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Employees;

namespace BiblioTechDomain.Services.IService
{
    public interface IEmployeeService : IBaseService<CreateEmployeeViewModel, UpdateEmployeeViewModel, ReadEmployeeViewModel, Employee>
    {
        Task<ReadEmployeeViewModel> SignInAsync(CreateEmployeeSignInViewModel createEmployeeSignIn);
        
        Task<ReadEmployeeViewModel> ChangePasswordAsync(UpdateEmployeePasswordViewModel updateEmployeePassword);
    }
}
