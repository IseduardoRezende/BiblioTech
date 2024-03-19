using BiblioTechDomain.Bases;

namespace BiblioTechDomain.Interfaces
{
    public interface IReadModel
    {
        long Id { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime? DeletedAt { get; set; }

        BaseValidation? Validation { get; init; }         
    }
}
