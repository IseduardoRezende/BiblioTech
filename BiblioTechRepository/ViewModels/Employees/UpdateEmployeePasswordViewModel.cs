namespace BiblioTechDomain.ViewModels.Employees
{
    public class UpdateEmployeePasswordViewModel : BaseUpdateViewModel
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordConfirmation { get; set; }    
    }
}
