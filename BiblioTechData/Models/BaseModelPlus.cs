using BiblioTechData.Interfaces;

namespace BiblioTechData.Models
{
    public class BaseModelPlus : BaseModel, IBaseModelPlus
    {
        public string Description { get; set; }
    }
}
