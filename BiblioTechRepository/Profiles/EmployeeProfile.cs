using AutoMapper;
using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Employees;

namespace BiblioTechDomain.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeViewModel, Employee>();
            CreateMap<UpdateEmployeeViewModel, Employee>();
            CreateMap<Employee, ReadEmployeeViewModel>();
        }
    }
}
