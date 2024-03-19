using System.Text.Json.Serialization;

namespace BiblioTechDomain.ViewModels
{
    public class BaseReadViewModelPlus : BaseReadViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Description { get; set; }
    }
}
