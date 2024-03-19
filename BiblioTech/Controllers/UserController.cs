using BiblioTechData.Enums;
using BiblioTechData.Models;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Controllers
{
    public class UserController : BaseController<CreateUserSignUpViewModel, UpdateUserViewModel, ReadUserViewModel, User>
    {
        private static readonly string[] includes = new[] { "Type" };                
        private readonly IUserService _userService;

        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        public override Task<IActionResult> ListAsync(
            [FromQuery(Name = "filter")] string filter = default!, 
            [FromQuery(Name = "orderField")] string orderField = "createdAt", 
            [FromQuery(Name = "orderType")] OrderType orderType = OrderType.Asc, 
            [FromQuery(Name = "offSet")] int offSet = 0, 
            [FromQuery(Name = "itemsPerPage")] short itemsPerPage = 15)
        {            
            return base.ListAsync(filter, orderField, orderType, offSet, itemsPerPage, includes);
        }

        public override Task<IActionResult> FindByIdAsync([FromRoute(Name = "id")] long id)
        {
            return base.FindByIdAsync(id, includes);
        }

        [HttpPost("SignUp")]
        public override async Task<IActionResult> CreateAsync([FromBody] CreateUserSignUpViewModel createModel)
        {
            return await base.CreateAsync(createModel);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody] CreateUserSignInViewModel signUpModel)
        {
            var connectedUser = await _userService.SignInAsync(signUpModel);

            return connectedUser.Validation != null
                ? UnprocessableEntity(connectedUser.Validation)
                : Ok(connectedUser);
        }

        [HttpPut("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePasswordAsync([FromRoute(Name = "id")] long id,
                                                             [FromBody] UpdateUserPasswordViewModel updateUserPassword)
        {
            if (id != updateUserPassword.Id)
                return BadRequest("Route Id is different from Body Id");

            if (updateUserPassword.NewPasswordConfirmation != updateUserPassword.NewPassword)
                return BadRequest("New Password and New Passoword Confirmation are different");

            var updatedUser = await _userService.ChangePasswordAsync(updateUserPassword);

            return updatedUser.Validation != null
                ? UnprocessableEntity(updatedUser.Validation)
                : Ok(updatedUser);
        }
    }
}
