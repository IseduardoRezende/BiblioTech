namespace BiblioTechDomain.ViewModels.Users
{
    public class UpdateUserPasswordViewModel : BaseUpdateViewModel
    {
        public string Password { get; set; }
        
        public string NewPassword { get; set; }

        public string NewPasswordConfirmation { get; set; }
    }
}
