namespace BiblioTechData.Models
{
    public class Functionality : BaseModelPlus
    {
        public Functionality()
        {
            Permissions = new List<Permission>();
        }

        public string Section { get; set; }
        
        public string Code { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
