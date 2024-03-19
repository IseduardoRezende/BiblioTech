using AutoMapper;
using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Bases;
using BiblioTechDomain.Enums;
using BiblioTechDomain.Extensions;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Employees;

namespace BiblioTechDomain.Services
{
    public class EmployeeService : BaseService<CreateEmployeeViewModel, UpdateEmployeeViewModel, ReadEmployeeViewModel, Employee>, IEmployeeService
    {
        private readonly ILibraryService _libraryService;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, ILibraryService libraryService, IMapper mapper)
            : base(employeeRepository, mapper)
        {
            _libraryService = libraryService;
            _employeeRepository = employeeRepository;
        }

        public async Task<ReadEmployeeViewModel> SignInAsync(CreateEmployeeSignInViewModel createEmployeeSignIn)
        {
            if (createEmployeeSignIn == null || string.IsNullOrEmpty(createEmployeeSignIn.Password))            
                return base.BuildReadModel(new BaseError(nameof(createEmployeeSignIn), createEmployeeSignIn, Error.Invalid_Value));            

            var employee = await base
                .FindByAsync(c => c.Email == createEmployeeSignIn.Email && c.Library.Code == createEmployeeSignIn.LibraryCode, includes: "Library");

            if (employee == null)            
                return base.BuildReadModel(new BaseError(nameof(createEmployeeSignIn), createEmployeeSignIn, Error.Non_Existent_Value));                            

            var inputPassword = createEmployeeSignIn.Password;
            createEmployeeSignIn.Password = createEmployeeSignIn.Password.HashPassword(employee.Salt.SaltPassword());

            if (createEmployeeSignIn.Password != employee.Password)            
                return base.BuildReadModel(new BaseError(nameof(createEmployeeSignIn.Password), inputPassword, Error.Non_Existent_Value));                             

            return employee;
        }

        public override async Task<ReadEmployeeViewModel> CreateAsync(CreateEmployeeViewModel createModel)
        {
            if (createModel == null || !createModel.Email.IsValidEmail())
                return base.BuildReadModel(new BaseError(nameof(createModel), createModel, Error.Invalid_Value))!;

            createModel.Name = createModel.Email.Split('@').First();

            createModel.Salt = Guid.NewGuid().ToString();
            createModel.Password = createModel.Salt[..8].HashPassword(createModel.Salt.SaltPassword());
            return await base.CreateAsync(createModel);
        }

        public async Task<ReadEmployeeViewModel> ChangePasswordAsync(UpdateEmployeePasswordViewModel updateEmployeePassword)
        {
            if (updateEmployeePassword == null || string.IsNullOrEmpty(updateEmployeePassword.Password) || string.IsNullOrEmpty(updateEmployeePassword.NewPassword))
                return base.BuildReadModel(new BaseError(nameof(updateEmployeePassword), updateEmployeePassword, Error.Invalid_Value));

            var employee = await _employeeRepository.FindByAsync(c => c.Id == updateEmployeePassword.Id);

            if (employee == null)
                return base.BuildReadModel(new BaseError(nameof(updateEmployeePassword.Id), updateEmployeePassword.Id, Error.Non_Existent_Value));

            var inputPassword = updateEmployeePassword.Password;
            updateEmployeePassword.Password = updateEmployeePassword.Password.HashPassword(employee.Salt.SaltPassword());

            if (updateEmployeePassword.Password != employee.Password)
                return base.BuildReadModel(new BaseError(nameof(updateEmployeePassword.Password), inputPassword, Error.Non_Existent_Value));

            employee.Salt = Guid.NewGuid().ToString();
            employee.Password = updateEmployeePassword.NewPassword.HashPassword(employee.Salt.SaltPassword());

            var updatedEmployee = await _employeeRepository.UpdateAsync(employee);
            return _mapper.Map<ReadEmployeeViewModel>(updatedEmployee);
        }

        protected override Func<Employee, bool> Filter(IEnumerable<BaseFilter> filters)
        {
            var name = filters.FirstOrDefault(c => string.Equals(c.Field, "name", StringComparison.OrdinalIgnoreCase));
            var hasName = name == null ? false : true;
            var nameValue = hasName ? name!.Value : string.Empty;

            var email = filters.FirstOrDefault(c => string.Equals(c.Field, "email", StringComparison.OrdinalIgnoreCase));
            var hasEmail = email == null ? false : true;
            var emailValue = hasEmail ? email!.Value : string.Empty;

            var status = filters.FirstOrDefault(c => string.Equals(c.Field, "status", StringComparison.OrdinalIgnoreCase));
            int statusType = 0;
            var hasStatus = status == null ? false : int.TryParse(status.Value, out statusType);

            return a =>
            (!hasName || string.IsNullOrEmpty(nameValue) || string.Equals(a.Name, nameValue, StringComparison.OrdinalIgnoreCase)) &&
            (!hasEmail || string.IsNullOrEmpty(emailValue) || string.Equals(a.Email, emailValue, StringComparison.OrdinalIgnoreCase)) &&
            (!hasStatus || BaseFilter.ApplyStatusFilter(a, statusType));
        }

        protected override async Task<BaseValidation> IsValidCreate(CreateEmployeeViewModel createModel)
        {
            var validation = new BaseValidation();

            if (createModel == null)
                validation.AddError(new BaseError(nameof(createModel), createModel, Error.Null_Value));

            var library = await _libraryService.FindByAsync(c => c.Id == createModel!.LibraryId);

            if (library == null)
                validation.AddError(new BaseError(nameof(createModel.LibraryId), createModel!.LibraryId, Error.Non_Existent_Value));

            var employee = base.FindByAsync(c => string.Equals(c.Email, createModel!.Email, StringComparison.OrdinalIgnoreCase));

            if (employee != null || !createModel!.Email.IsValidEmail())
                validation.AddError(new BaseError(nameof(createModel.Email), createModel!.Email, Error.Invalid_Value));

            return validation;
        }

        protected override async Task<BaseValidation> IsValidUpdate(UpdateEmployeeViewModel updateModel)
        {
            var validation = new BaseValidation();

            if (updateModel == null)
                validation.AddError(new BaseError(nameof(updateModel), updateModel, Error.Null_Value));

            if (string.IsNullOrEmpty(updateModel!.Name))
                validation.AddError(new BaseError(nameof(updateModel.Name), updateModel.Name, Error.Invalid_Value));

            var employee = await base.FindByAsync(c => string.Equals(c.Email, updateModel.Email, StringComparison.OrdinalIgnoreCase));

            if ((employee != null && employee.Id != updateModel.Id) || !updateModel.Email.IsValidEmail())
                validation.AddError(new BaseError(nameof(updateModel.Email), updateModel.Email, Error.Invalid_Value));

            return validation;
        }

        protected override Employee UpdateProperties(Employee model, UpdateEmployeeViewModel updateModel)
        {
            model.Name = updateModel.Name;
            model.Email = updateModel.Email;
            return model;
        }
    }
}
