using BiblioTechDomain.ViewModels.Permissions;

namespace BiblioTechDomain.ViewModels.Users
{
    public class ReadConnectedUserViewModel : BaseReadViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public long TypeId { get; set; }

        public string TypeDescription { get; set; }
        
        public ReadPermissionViewModel[] Permissions { get; set; }
    }
}
