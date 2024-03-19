using BiblioTechData.Enums;
using BiblioTechData.Models;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Controllers
{
    public class EmployeeController : BaseController<CreateEmployeeViewModel, UpdateEmployeeViewModel, ReadEmployeeViewModel, Employee>
    {
        private static readonly string[] includes = new[] { "Library" };
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
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

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody] CreateEmployeeSignInViewModel createEmployeeSignIn)
        {
            var connectedEmployee = await _employeeService.SignInAsync(createEmployeeSignIn);

            return connectedEmployee.Validation != null
                ? UnprocessableEntity(connectedEmployee.Validation)
                : Ok(connectedEmployee);
        }       

        [HttpPut("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePasswordAsync([FromRoute(Name = "id")] long id, 
                                                             [FromBody] UpdateEmployeePasswordViewModel updateEmployeePassword)
        {
            if (id != updateEmployeePassword.Id)
                return BadRequest("Route Id is different from Body Id");

            if (updateEmployeePassword.NewPassword != updateEmployeePassword.NewPasswordConfirmation)
                return BadRequest("New Password and New Password Confirmation are different");

            var updatedEmployee = await _employeeService.ChangePasswordAsync(updateEmployeePassword);

            return updatedEmployee.Validation != null
                ? UnprocessableEntity(updatedEmployee.Validation)
                : Ok(updatedEmployee);    
        }
    }
}
