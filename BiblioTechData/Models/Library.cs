namespace BiblioTechData.Models
{
    public class Library : BaseModel
    {
        public Library()
        {
            Books = new List<Book>();
            Employees = new List<Employee>();
        }

        public string Name { get; set; }
        
        public int Code { get; set; } 

        public string? Image { get; set; }

        public long UserId { get; set; }

        public string Address { get; set; }

        public string Number { get; set; }

        public string? Complement { get; set; }

        public string City { get; set; }

        public string UF { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public User User { get; set; }

        public ICollection<Book> Books { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
