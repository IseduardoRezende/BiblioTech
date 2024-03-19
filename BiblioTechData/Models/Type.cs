global using Type = BiblioTechData.Models.Type;

namespace BiblioTechData.Models
{
    public class Type : BaseModelPlus
    {
        public Type()
        {
            Users = new List<User>();
            Permissions = new List<Permission>();
        }

        public ICollection<User> Users { get; set; }
        
        public ICollection<Permission> Permissions { get; set; }
    }
}
