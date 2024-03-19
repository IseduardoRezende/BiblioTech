using AutoMapper;
using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Bases;
using BiblioTechDomain.Enums;
using BiblioTechDomain.Extensions;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Libraries;
using TypeEnum = BiblioTechDomain.Enums.Type;

namespace BiblioTechDomain.Services
{
    public class LibraryService : BaseService<CreateLibraryViewModel, UpdateLibraryViewModel, ReadLibraryViewModel, Library>, ILibraryService
    {
        private readonly IUserService _userService;

        public LibraryService(ILibraryRepository libraryRepository, IUserService userService, IMapper mapper)
            : base(libraryRepository, mapper)
        {
            _userService = userService;
        }

        public override async Task<ReadLibraryViewModel> CreateAsync(CreateLibraryViewModel createModel)
        {
            if (createModel == null)
                return base.BuildReadModel(new BaseError(nameof(createModel), createModel, Error.Invalid_Value))!;

            createModel.Code = Random.Shared.Next(1000, 10000);
            return await base.CreateAsync(createModel);
        }        

        protected override Func<Library, bool> Filter(IEnumerable<BaseFilter> filters)
        {
            var name = filters.FirstOrDefault(c => string.Equals(c.Field, "name", StringComparison.OrdinalIgnoreCase));
            var hasName = name == null ? false : true;
            var nameValue = hasName ? name!.Value : string.Empty;

            var address = filters.FirstOrDefault(c => string.Equals(c.Field, "address", StringComparison.OrdinalIgnoreCase));
            var hasAddress = address == null ? false : true;
            var addressValue = hasAddress ? address!.Value : string.Empty;

            var number = filters.FirstOrDefault(c => string.Equals(c.Field, "number", StringComparison.OrdinalIgnoreCase));
            var hasNumber = number == null ? false : true;
            var numberValue = hasNumber ? number!.Value : string.Empty;

            var city = filters.FirstOrDefault(c => string.Equals(c.Field, "city", StringComparison.OrdinalIgnoreCase));
            var hasCity = city == null ? false : true;
            var cityValue = hasCity ? city!.Value : string.Empty;

            var uf = filters.FirstOrDefault(c => string.Equals(c.Field, "uf", StringComparison.OrdinalIgnoreCase));
            var hasUf = uf == null ? false : true;
            var ufValue = hasUf ? uf!.Value : string.Empty;

            var postalCode = filters.FirstOrDefault(c => string.Equals(c.Field, "postalCode", StringComparison.OrdinalIgnoreCase));
            var hasPostalCode = postalCode == null ? false : true;
            var postalCodeValue = hasPostalCode ? postalCode!.Value : string.Empty;

            var phone = filters.FirstOrDefault(c => string.Equals(c.Field, "phone", StringComparison.OrdinalIgnoreCase));
            var hasPhone = phone == null ? false : true;
            var phoneValue = hasPhone ? phone!.Value : string.Empty;

            return a =>
                (!hasName || string.IsNullOrEmpty(nameValue) || string.Equals(a.Name, nameValue, StringComparison.OrdinalIgnoreCase)) &&
                (!hasAddress || string.IsNullOrEmpty(addressValue) || string.Equals(a.Address, addressValue, StringComparison.OrdinalIgnoreCase)) &&
                (!hasNumber || string.IsNullOrEmpty(numberValue) || a.Number == numberValue) &&
                (!hasCity || string.IsNullOrEmpty(cityValue) || a.City == cityValue) &&
                (!hasUf || string.IsNullOrEmpty(ufValue) || a.UF == ufValue) &&
                (!hasPostalCode || string.IsNullOrEmpty(postalCodeValue) || a.PostalCode == postalCodeValue) &&
                (!hasPhone || string.IsNullOrEmpty(phoneValue) || a.Phone == phoneValue);
        }

        protected override async Task<BaseValidation> IsValidCreate(CreateLibraryViewModel createModel)
        {
            var validation = new BaseValidation();

            if (createModel == null)
                validation.AddError(new BaseError(nameof(createModel), createModel, Error.Null_Value));

            var library = await base.FindByAsync(c => c.Name == createModel!.Name);
            
            if (library != null || string.IsNullOrEmpty(createModel!.Name))
                validation.AddError(new BaseError(nameof(createModel.Name), createModel!.Name, Error.Invalid_Value));
            
            var user = await _userService.FindByAsync(c => c.Id == createModel!.UserId);

            if (user == null || user.TypeId != (int)TypeEnum.Manager)
                validation.AddError(new BaseError(nameof(createModel.UserId), createModel.UserId, Error.Invalid_Value));

            if (string.IsNullOrEmpty(createModel.Address))
                validation.AddError(new BaseError(nameof(createModel.Address), createModel.Address, Error.Invalid_Value));

            if (string.IsNullOrEmpty(createModel.Number))
                validation.AddError(new BaseError(nameof(createModel.Number), createModel.Number, Error.Invalid_Value));

            if (string.IsNullOrEmpty(createModel.City))
                validation.AddError(new BaseError(nameof(createModel.City), createModel.City, Error.Invalid_Value));

            if (string.IsNullOrEmpty(createModel.UF))
                validation.AddError(new BaseError(nameof(createModel.UF), createModel.UF, Error.Invalid_Value));

            if (string.IsNullOrEmpty(createModel.PostalCode))
                validation.AddError(new BaseError(nameof(createModel.PostalCode), createModel.PostalCode, Error.Invalid_Value));

            if (!createModel.Phone.IsValidPhone())
                validation.AddError(new BaseError(nameof(createModel.Phone), createModel.Phone, Error.Invalid_Value));

            return validation;
        }

        protected override async Task<BaseValidation> IsValidUpdate(UpdateLibraryViewModel updateModel)
        {
            var validation = new BaseValidation();

            if (updateModel == null)
                validation.AddError(new BaseError(nameof(updateModel), updateModel, Error.Null_Value));

            var library = await base.FindByAsync(c => c.Name == updateModel!.Name);

            if ((library != null && library.Id != updateModel!.Id) || string.IsNullOrEmpty(updateModel!.Name))
                validation.AddError(new BaseError(nameof(updateModel.Name), updateModel.Name, Error.Invalid_Value));

            if (string.IsNullOrEmpty(updateModel.Address))
                validation.AddError(new BaseError(nameof(updateModel.Address), updateModel.Address, Error.Invalid_Value));

            if (string.IsNullOrEmpty(updateModel.Number))
                validation.AddError(new BaseError(nameof(updateModel.Number), updateModel.Number, Error.Invalid_Value));

            if (string.IsNullOrEmpty(updateModel.City))
                validation.AddError(new BaseError(nameof(updateModel.City), updateModel.City, Error.Invalid_Value));

            if (string.IsNullOrEmpty(updateModel.UF))
                validation.AddError(new BaseError(nameof(updateModel.UF), updateModel.UF, Error.Invalid_Value));

            if (string.IsNullOrEmpty(updateModel.PostalCode))
                validation.AddError(new BaseError(nameof(updateModel.PostalCode), updateModel.PostalCode, Error.Invalid_Value));

            if (!updateModel.Phone.IsValidPhone())
                validation.AddError(new BaseError(nameof(updateModel.Phone), updateModel.Phone, Error.Invalid_Value));

            return validation;
        }

        protected override Library UpdateProperties(Library model, UpdateLibraryViewModel updateModel)
        {
            model.Name = updateModel.Name;
            model.Address = updateModel.Address;
            model.Number = updateModel.Number;
            model.Complement = updateModel.Complement;
            model.City = updateModel.City;
            model.UF = updateModel.UF;
            model.PostalCode = updateModel.PostalCode;
            model.Phone = updateModel.Phone;

            return model;
        }
    }
}
