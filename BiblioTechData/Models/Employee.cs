namespace BiblioTechData.Models
{
    public class Employee : BaseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        
        public string Salt { get; set; }

        public long LibraryId { get; set; }

        public Library Library { get; set; }
    }
}
