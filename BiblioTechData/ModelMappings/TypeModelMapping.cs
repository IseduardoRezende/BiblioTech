using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioTechData.ModelMappings
{
    public class TypeModelMapping : BaseModelMappingPlus<Type>
    {
        public TypeModelMapping(int descriptionMaxLength) : base(descriptionMaxLength) { }

        public override void Configure(EntityTypeBuilder<Type> entity)
        {
            entity.ToTable("Type");
            base.Configure(entity);            
        }
    }
}
