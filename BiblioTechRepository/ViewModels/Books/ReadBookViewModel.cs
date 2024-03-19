namespace BiblioTechDomain.ViewModels.Books
{
    public class ReadBookViewModel : BaseReadViewModel
    {
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ISBN { get; set; }

        public int? Pages { get; set; }

        public int Exemplary { get; set; }

        public int Volume { get; set; }

        public string? Image { get; set; }

        public long GenreId { get; set; }

        public string GenreDescription { get; set; }

        public long LibraryId { get; set; }

        public string LibraryName { get; set; }
    }
}
