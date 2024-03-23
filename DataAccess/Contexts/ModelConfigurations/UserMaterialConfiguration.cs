using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EducationalPortal.DataAccess.Models.Entities;

namespace EducationalPortal.DataAccess.Contexts.ModelConfigurations
{
    public class UserMaterialConfiguration : IEntityTypeConfiguration<UserMaterial>
    {
        public void Configure(EntityTypeBuilder<UserMaterial> builder)
        {
            builder.HasKey(um => new { um.UserId, um.MaterialId });

            builder.HasOne(um => um.User)
                .WithMany(u => u.UserMaterials)
                .HasForeignKey(um => um.UserId);

            builder.HasOne(um => um.Material)
                .WithMany(m => m.UserMaterials)
                .HasForeignKey(um => um.MaterialId);

            builder.Property(um => um.IsCompleted)
                .IsRequired();
        }
    }
}
