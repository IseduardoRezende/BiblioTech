namespace BiblioTechDomain.ViewModels.Libraries
{
    public class ReadLibraryViewModel : BaseReadViewModel 
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public string? Image { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string Number { get; set; }

        public string? Complement { get; set; }

        public string City { get; set; }

        public string UF { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }
    }
}
