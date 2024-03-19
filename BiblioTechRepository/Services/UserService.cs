using AutoMapper;
using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Users;
using BiblioTechDomain.Enums;
using BiblioTechDomain.Bases;
using BiblioTechDomain.Extensions;

namespace BiblioTechDomain.Services
{
    public class UserService : BaseService<CreateUserSignUpViewModel, UpdateUserViewModel, ReadUserViewModel, User>, IUserService
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository,
                           IPermissionService permissionService,
                           IMapper mapper) : base(userRepository, mapper)
        {
            _permissionService = permissionService;
            _userRepository = userRepository;
        }

        public override async Task<ReadUserViewModel> CreateAsync(CreateUserSignUpViewModel createModel)
        {
            if (createModel == null || !createModel.Email.IsValidEmail())
                return base.BuildReadModel(new BaseError(nameof(createModel), createModel, Error.Invalid_Value))!;

            createModel.Name = createModel.Email.Split('@').First();
            createModel.Salt = Guid.NewGuid().ToString();
            createModel.Password = createModel.Password.HashPassword(createModel.Salt.SaltPassword());

            return await base.CreateAsync(createModel);
        }

        public async Task<ReadConnectedUserViewModel> SignInAsync(CreateUserSignInViewModel createSignInViewModel)
        {
            if (createSignInViewModel == null || string.IsNullOrEmpty(createSignInViewModel.Password))
            {
                return new ReadConnectedUserViewModel
                {
                    Validation = new BaseValidation(new BaseError(nameof(createSignInViewModel), createSignInViewModel, Error.Invalid_Value))
                };
            }

            var user = await base.FindByAsync(c => string.Equals(c.Email, createSignInViewModel.Email, StringComparison.OrdinalIgnoreCase), "Type");

            if (user == null)
            {
                return new ReadConnectedUserViewModel
                {
                    Validation = new BaseValidation(new BaseError(nameof(createSignInViewModel.Email), createSignInViewModel.Email, Error.Non_Existent_Value))
                };
            }

            var enterPassword = createSignInViewModel.Password;
            createSignInViewModel.Password = createSignInViewModel.Password.HashPassword(user.Salt.SaltPassword());

            if (createSignInViewModel.Password == user.Password)
            {
                return new ReadConnectedUserViewModel 
                { 
                    Validation = new BaseValidation(new BaseError(nameof(createSignInViewModel.Password), enterPassword, Error.Non_Existent_Value)) 
                };
            }

            return await SetConnectedUserAsync(user);
        }

        private async Task<ReadConnectedUserViewModel> SetConnectedUserAsync(ReadUserViewModel readUserViewModel)
        {
            if (readUserViewModel == null) return default!;

            var connectedUser = _mapper.Map<ReadConnectedUserViewModel>(readUserViewModel);

            var permissions = await _permissionService.GetPermissionsAsync(readUserViewModel.TypeId);
            connectedUser.Permissions = permissions.ToArray();
            return connectedUser;
        }

        public async Task<ReadUserViewModel> ChangePasswordAsync(UpdateUserPasswordViewModel updateUserPassword)
        {
            if (updateUserPassword == null || string.IsNullOrEmpty(updateUserPassword.NewPassword) || string.IsNullOrEmpty(updateUserPassword.Password))
                return base.BuildReadModel(new BaseError(nameof(updateUserPassword), updateUserPassword, Error.Invalid_Value));

            var user = await _userRepository.FindByAsync(c => c.Id == updateUserPassword.Id);

            if (user == null)
                return base.BuildReadModel(new BaseError(nameof(updateUserPassword.Id), updateUserPassword.Id, Error.Non_Existent_Value));

            var enterPassword = updateUserPassword.Password;
            updateUserPassword.Password = updateUserPassword.Password.HashPassword(user.Salt.SaltPassword());

            if (updateUserPassword.Password != user.Password)
                return base.BuildReadModel(new BaseError(nameof(updateUserPassword.Password), enterPassword, Error.Non_Existent_Value));

            user.Salt = Guid.NewGuid().ToString();
            user.Password = updateUserPassword.NewPassword.HashPassword(user.Salt.SaltPassword());

            var updatedUser = await _userRepository.UpdateAsync(user);
            return _mapper.Map<ReadUserViewModel>(updatedUser);
        }

        protected override Func<User, bool> Filter(IEnumerable<BaseFilter> filters)
        {
            var status = filters.FirstOrDefault(c => string.Equals(c.Field, "status", StringComparison.OrdinalIgnoreCase));
            int statusType = 0;
            var hasStatus = status == null ? false : int.TryParse(status.Value, out statusType);

            return a =>
            (!hasStatus || BaseFilter.ApplyStatusFilter(a, statusType));
        }

        protected async override Task<BaseValidation> IsValidCreate(CreateUserSignUpViewModel createModel)
        {
            var validation = new BaseValidation();

            if (createModel == null)
                validation.AddError(new BaseError(nameof(createModel), createModel, Error.Null_Value));

            var user = await base.FindByAsync(c => string.Equals(c.Email, createModel!.Email, StringComparison.OrdinalIgnoreCase));

            if (user != null || !createModel!.Email.IsValidEmail())
                validation.AddError(new BaseError(nameof(createModel.Email), createModel!.Email, Error.Invalid_Value));

            if (string.IsNullOrEmpty(createModel.Password))
                validation.AddError(new BaseError(nameof(createModel.Password), createModel.Password, Error.Invalid_Value));

            if (!createModel.TypeId.IsValidType())
                validation.AddError(new BaseError(nameof(createModel.TypeId), createModel.TypeId, Error.Invalid_Value));

            return validation;
        }

        protected async override Task<BaseValidation> IsValidUpdate(UpdateUserViewModel updateModel)
        {
            var validation = new BaseValidation();

            if (updateModel == null)
                validation.AddError(new BaseError(nameof(updateModel), updateModel, Error.Null_Value));

            if (string.IsNullOrEmpty(updateModel!.Name))
                validation.AddError(new BaseError(nameof(updateModel.Name), updateModel.Name, Error.Invalid_Value));

            var user = await base.FindByAsync(c => string.Equals(c.Email, updateModel!.Email, StringComparison.OrdinalIgnoreCase));

            if ((user != null && user.Id != updateModel.Id) || !updateModel.Email.IsValidEmail())
                validation.AddError(new BaseError(nameof(updateModel.Email), updateModel.Email, Error.Invalid_Value));

            if (!updateModel!.Phone.IsValidPhone())
                validation.AddError(new BaseError(nameof(updateModel.Phone), updateModel.Phone, Error.Invalid_Value));

            return validation;
        }

        protected override User UpdateProperties(User model, UpdateUserViewModel updateModel)
        {
            model.Name = updateModel.Name.Trim();
            model.Email = updateModel.Email;
            model.Phone = updateModel.Phone;
            return model;
        }
    }
}
