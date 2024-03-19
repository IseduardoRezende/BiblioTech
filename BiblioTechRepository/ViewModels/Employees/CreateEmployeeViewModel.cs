using System.Text.Json.Serialization;

namespace BiblioTechDomain.ViewModels.Employees
{
    public class CreateEmployeeViewModel : BaseCreateViewModel
    {
        [JsonIgnore]
        public string? Name { get; set; }
        
        public string Email { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
        
        [JsonIgnore]
        public string? Salt { get; set; }

        public long LibraryId { get; set; }
    }
}
