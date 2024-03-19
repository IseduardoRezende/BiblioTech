using System.Text.Json.Serialization;

namespace BiblioTechDomain.ViewModels.Employees
{
    public class ReadEmployeeViewModel : BaseReadViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        
        [JsonIgnore]
        public string Salt { get; set; }

        public long LibraryId { get; set; }

        public string LibraryName { get; set; }
    }
}
