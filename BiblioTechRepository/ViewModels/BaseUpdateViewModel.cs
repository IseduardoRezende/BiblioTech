using BiblioTechDomain.Interfaces;

namespace BiblioTechDomain.ViewModels
{
    public abstract class BaseUpdateViewModel : IUpdateModel
    {
        public long Id { get; set; }              
    }
}
