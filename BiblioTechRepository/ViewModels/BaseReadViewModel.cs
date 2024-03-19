using BiblioTechDomain.Interfaces;
using BiblioTechDomain.Bases;
using System.Text.Json.Serialization;

namespace BiblioTechDomain.ViewModels
{
    public abstract class BaseReadViewModel : IReadModel
    {           
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public long Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BaseValidation? Validation { get; init; }
    }
}
