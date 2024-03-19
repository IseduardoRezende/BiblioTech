namespace BiblioTechDomain.ViewModels.Libraries
{
    public class UpdateLibraryViewModel : BaseUpdateViewModel
    {        
        public string Name { get; set; }

        public string Address { get; set; }

        public string Number { get; set; }

        public string? Complement { get; set; }

        public string City { get; set; }

        public string UF { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }
    }
}
