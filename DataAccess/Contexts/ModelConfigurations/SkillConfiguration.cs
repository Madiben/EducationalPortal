using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EducationalPortal.DataAccess.Enums;
using EducationalPortal.DataAccess.Models.Entities;

namespace EducationalPortal.DataAccess.Contexts.ModelConfigurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(x => x.SkillId);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(u => u.Level)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(),
                        v => (SkillLevel)Enum.Parse(typeof(SkillLevel), v));


        }
    }
}
