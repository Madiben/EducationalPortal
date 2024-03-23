using EducationalPortal.BusinessLogic.Services.Interfaces;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.DataAccess.Repositories.Classes;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using EducationalPortal.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using EducationalPortal.DataAccess.Contexts;

namespace EducationalPortal.BusinessLogic.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository materialRepository;
        private readonly IUserRepository userRepository;
        private readonly ICourseRepository courseRepository;
        private readonly EducationalPortalContext _context;

        public MaterialService(EducationalPortalContext dbContext)
        {
            _context = dbContext;
            materialRepository = new MaterialRepository(_context);
            courseRepository = new CourseRepository(_context);
            userRepository = new UserRepository(_context);
        }

        public void FillMaterialTable()
        {
            var materials = new List<Material>
            {
                new Book
                {
                    Name = "C# in Depth",
                    Description = "A deep dive into C#.",
                    Type = MaterialType.Book,
                    Format = "PDF",
                    Authors = new List<string> { "Jon Skeet" },
                    YearOfPublication = 2019,
                },
                new Video
                {
                    Name = "C# Fundamentals",
                    Description = "Video series on C# basics.",
                    Type = MaterialType.Video,
                    DurationInMinutes = 120,
                },
                new Book  { Name = "Effective C#", Description = "Covers best practices in C# programming", Authors = new List<string> { "Bill Wagner" }, Format = "PDF", Type = MaterialType.Book, Pages = 350, YearOfPublication = 2019},
                new Book  { Name = "Concurrency in C# Cookbook", Description = "Asynchronous and parallel programming in C#", Authors = new List<string> { "Stephen Cleary" }, Format = "PDF", Type = MaterialType.Book, Pages = 210, YearOfPublication = 2020},
                new Book  { Name = "Learning C#", Description = "A beginner's guide to C#", Authors = new List<string> { "Author 1","Author 2" }, Format = "PDF", Type = MaterialType.Book, Pages = 300, YearOfPublication = 2020},
                new Video { Name = "Introduction to ASP.NET Core", Description = "Beginner's guide to web development with ASP.NET Core", Type = MaterialType.Video, DurationInMinutes = 180, Quality = "1080p" },
                new Video { Name = "Mastering LINQ in C#", Description = "Comprehensive guide to using LINQ", Type = MaterialType.Video, DurationInMinutes = 120, Quality = "720p" },
                new Video { Name = "Understanding Entity Framework", Description = "Entity Framework for database access in C#", Type = MaterialType.Video, DurationInMinutes = 150, Quality = "1080p" },
                new Article { Name = "Latest Trends in C#", Description = "Exploring the new features in C# 9.0", Source = "DotNet Magazine", Author = "Alice Johnson", PublicationDate = new DateTime(2021, 1, 20) },
                new Article { Name = "Async/Await Best Practices", Description = "How to effectively use async/await in C#", Source = "C# Corner", Author = "Bob Smith", PublicationDate = new DateTime(2021, 6, 15) },
                new Article { Name = "Microservices with .NET 5", Description = "Building scalable microservices using .NET 5", Source = "Tech World", Author = "Carol White", PublicationDate = new DateTime(2021, 3, 10) }
            };

            materialRepository.AddRange(materials);
            materialRepository.Save();
        }

        public void AddMaterial(Material material)
        {
            materialRepository.Add(material);
        }

        public void AddMaterialToCourse(Material material, int courseId)
        {
            var course = courseRepository.Get(courseId);

            if (course != null)
            {
                course.Materials.Add(material);
                courseRepository.Update(course);
            }
            else
            {
                throw new InvalidOperationException("Course not found");
            }
            _context.SaveChanges();
        }

        public void RemoveMaterialFromCourse(Material material, int courseId)
        {
            var course = courseRepository.Get(courseId);

            if (course != null)
            {
                course.Materials.Remove(material);
                courseRepository.Update(course);
            }
            else
            {
                throw new InvalidOperationException("Course not found");
            }
        }

        public void DeleteMaterial(int materialId)
        {
            materialRepository.Delete(GetMaterialById(materialId));
        }

        public Material GetMaterialById(int materialId)
        {
            return materialRepository.Get(materialId);
        }

        public IEnumerable<Material> GetMaterialList()
        {
            return materialRepository.GetAll();
        }

        public void UpdateMaterial(Material material)
        {
            materialRepository.Update(material);
        }

        public void EnrollUserInMaterial(int userId, int materialId)
        {
            var user = userRepository.Get(userId);
            var material = materialRepository.Get(materialId);

            if (user == null || materialId == null)
            {
                throw new ArgumentException("User or Material not found.");
            }

            var userMaterial = new UserMaterial { UserId = userId, MaterialId = materialId, IsCompleted = false };
            _context.UserMaterials.Add(userMaterial);
            _context.SaveChanges();
        }

        public void UpdateUserMaterial(UserMaterial userMaterial)
        {
            _context.UserMaterials.Update(userMaterial);
        }

        public void CompleteMaterial(int userId, int materialId)
        {
            var userMaterial = _context.UserMaterials
                .FirstOrDefault(um => um.UserId == userId && um.MaterialId == materialId);

            if (userMaterial == null)
            {
                throw new ArgumentException("User or Material not found.");
            }

            userMaterial.IsCompleted = true;

            UpdateUserMaterial(userMaterial);
            _context.SaveChanges();
        }

        public void UpdateCourseCompelationPercentage(int userId, int courseId)
        {
            var userCourse = _context.UserCourses
                .FirstOrDefault(uc => uc.UserId == userId && uc.CourseId == courseId);

            if (userCourse != null)
            {
                var totalMaterials = userCourse.Course.Materials.Count;

                if (totalMaterials > 0)
                {
                    var completedMaterials = userCourse.Course.Materials
                            .Count(m => _context.UserMaterials
                                .Any(um => um.UserId == userId && um.MaterialId == m.MaterialId && um.IsCompleted));

                    var percentage = (double)completedMaterials / totalMaterials * 100;
                    userCourse.CompletionPercentage = percentage;
                }
                else
                {
                    userCourse.CompletionPercentage = 0;
                }

                _context.UserCourses.Update(userCourse);
                _context.SaveChanges();
            }
        }

        public void DropMaterialsForUser(int userId, int materialId)
        {
            var userMaterial = _context.UserMaterials
                .FirstOrDefault(um => um.UserId == userId && um.MaterialId == materialId);

            if (userMaterial != null)
            {
                _context.UserMaterials.Remove(userMaterial);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("User or Material not found.");
            }
        }
    }
}

