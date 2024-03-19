using BiblioTechData.Interfaces;

namespace BiblioTechData.Models
{
    public abstract class BaseModel : IBaseModel
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
