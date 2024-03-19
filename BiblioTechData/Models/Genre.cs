namespace BiblioTechData.Models
{
    public class Genre : BaseModelPlus
    {
        public Genre()
        {
            Books = new List<Book>();            
        }

        public ICollection<Book> Books { get; set; }
    }
}
