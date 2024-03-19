using System.Text.Json.Serialization;

namespace BiblioTechDomain.ViewModels.Users
{
    public class CreateUserSignUpViewModel : BaseCreateViewModel
    {
        [JsonIgnore]        
        public string? Name { get; set; }

        public string Email { get; set; }
      
        public string Password { get; set; }

        [JsonIgnore]
        public string? Salt { get; set; }

        public long TypeId { get; set; }
    }
}
