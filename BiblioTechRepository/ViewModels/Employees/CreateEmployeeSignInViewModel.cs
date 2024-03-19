namespace BiblioTechDomain.ViewModels.Employees
{
    public class CreateEmployeeSignInViewModel : BaseCreateViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int LibraryCode { get; set; }
    }
}
