using System.Text.Json.Serialization;

namespace BiblioTechDomain.ViewModels.Users
{
    public class ReadUserViewModel : BaseReadViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public string Salt { get; set; }

        public long TypeId { get; set; }
        
        public string? TypeDescription { get; set; }
    }
}
