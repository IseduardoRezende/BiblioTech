namespace BiblioTechDomain.ViewModels.Books
{
    public class UpdateBookViewModel : BaseUpdateViewModel
    {
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ISBN { get; set; }

        public int? Pages { get; set; }

        public int Exemplary { get; set; }

        public int Volume { get; set; }

        public long GenreId { get; set; }
    }
}
