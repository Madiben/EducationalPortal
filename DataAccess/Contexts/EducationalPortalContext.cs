using EducationalPortal.DataAccess.Contexts.ModelConfigurations;
using EducationalPortal.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationalPortal.DataAccess.Contexts
{
    public class EducationalPortalContext : DbContext
    {
        public DbSet<Login> Logins { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<UserCourse> UserCourses { get; set; }

        public DbSet<UserMaterial> UserMaterials { get; set; }

        public EducationalPortalContext(DbContextOptions options):base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());

            modelBuilder.ApplyConfiguration(new MaterialConfiguration());

            modelBuilder.ApplyConfiguration(new SkillConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new UserCourseConfiguration());

            modelBuilder.ApplyConfiguration(new UserMaterialConfiguration());

            modelBuilder.ApplyConfiguration(new LoginConfiguration());
        }

    }
}


