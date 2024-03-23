using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EducationalPortal.DataAccess.Models.Entities;

namespace EducationalPortal.DataAccess.Contexts.ModelConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.CourseId);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(a => a.Credit)
                .IsRequired();

            builder.HasMany(c => c.Skills)
                .WithMany(s => s.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseSkill",
                    j => j.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                    j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId")
                );
            builder.HasMany(c => c.Materials)
                .WithMany(m => m.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseMaterial",
                    j => j.HasOne<Material>().WithMany().HasForeignKey("MaterialId"),
                    j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId")
                );


        }
    }
}
