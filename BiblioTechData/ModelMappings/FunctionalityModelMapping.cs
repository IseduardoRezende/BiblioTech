using BiblioTechData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioTechData.ModelMappings
{
    public class FunctionalityModelMapping : BaseModelMappingPlus<Functionality>
    {
        public FunctionalityModelMapping(int descriptionMaxLength) : base(descriptionMaxLength) { }

        public override void Configure(EntityTypeBuilder<Functionality> entity)
        {
            entity.ToTable("Functionality");
            base.Configure(entity);

            entity
                .Property(c => c.Section)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("Section");

            entity
                .Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(6)
                .HasColumnName("Code");
        }
    }
}
