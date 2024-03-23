using EducationalPortal.DataAccess.Enums;
using EducationalPortal.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalPortal.DataAccess.Contexts.ModelConfigurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(x => x.MaterialId);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.HasDiscriminator<MaterialType>("MaterialType")
                .HasValue<Book>(MaterialType.Book)
                .HasValue<Article>(MaterialType.Article)
                .HasValue<Video>(MaterialType.Video);
        }
    }
}
