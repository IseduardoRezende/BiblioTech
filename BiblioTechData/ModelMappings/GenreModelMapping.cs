using BiblioTechData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioTechData.ModelMappings
{
    public class GenreModelMapping : BaseModelMappingPlus<Genre>
    {
        public GenreModelMapping(int descriptionMaxLength) : base(descriptionMaxLength) { }

        public override void Configure(EntityTypeBuilder<Genre> entity)
        {
            entity.ToTable("Genre");
            base.Configure(entity);
        }
    }
}
