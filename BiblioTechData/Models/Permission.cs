namespace BiblioTechData.Models
{
    public class Permission : BaseModel
    {       
        public short Level { get; set; }

        public long FunctionalityId { get; set; }

        public long TypeId { get; set; }

        public Type Type { get; set; }

        public Functionality Functionality { get; set; }
    }
}
