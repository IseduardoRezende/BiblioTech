using BiblioTechData.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioTechData.ModelMappings
{
    public abstract class BaseModelMappingPlus<ModelPlus> : BaseModelMapping<ModelPlus>
        where ModelPlus : class, IBaseModelPlus
    {
        private readonly int _descriptionMaxLength;

        public BaseModelMappingPlus(int descriptionMaxLength)
        {
            _descriptionMaxLength = descriptionMaxLength;
        }

        public override void Configure(EntityTypeBuilder<ModelPlus> entity)
        {
            base.Configure(entity);
            
            entity
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(_descriptionMaxLength)
                .HasColumnName("Description");
        }
    }
}
