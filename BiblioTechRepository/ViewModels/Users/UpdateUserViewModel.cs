namespace BiblioTechDomain.ViewModels.Users
{
    public class UpdateUserViewModel : BaseUpdateViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }        
    }
}
