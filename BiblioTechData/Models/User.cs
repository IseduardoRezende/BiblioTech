namespace BiblioTechData.Models
{
    public class User : BaseModel
    {
        public User()
        {
            Libraries = new List<Library>();
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public long TypeId { get; set; }

        public Type Type { get; set; }

        public ICollection<Library> Libraries { get; set; }
    }
}
