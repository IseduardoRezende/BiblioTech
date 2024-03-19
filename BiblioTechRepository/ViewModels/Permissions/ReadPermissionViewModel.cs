using System.Text.Json.Serialization;

namespace BiblioTechDomain.ViewModels.Permissions
{
    public class ReadPermissionViewModel : BaseReadViewModel
    {
        [JsonIgnore]
        public short Level { get; set; }

        public long FunctionalityId { get; set; }

        public string? FunctionalityCode { get; set; }

        public long TypeId { get; set; }
    }
}
