using EducationalPortal.DataAccess.Repositories.Classes;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using EducationalPortal.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using EducationalPortal.DataAccess.Contexts;
using EducationalPortal.BusinessLogic.Services.Interfaces;

namespace EducationalPortal.BusinessLogic.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IUserRepository userRepository;
        private readonly EducationalPortalContext _context;

        public CourseService(EducationalPortalContext dbContext)
        {
            _context = dbContext;
            courseRepository = new CourseRepository(_context);
            userRepository = new UserRepository(_context);
        }

        public void FillCourseTable()
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Name = "Introduction to C#",
                    Description = "Covers basic C# programming concepts, syntax, and features.",
                    Credit = 3
                },
                new Course
                {
                    Name = "Advanced C# Programming",
                    Description = "Delves into advanced topics like LINQ, async programming, and more.",
                    Credit = 4
                },
                new Course {
                    Name = "Introduction to Programming",
                    Description = "Basic programming concepts",
                    Credit = 3
                },
                new Course {
                    Name = "Advanced Databases",
                    Description = "Deep dive into SQL and NoSQL databases",
                    Credit = 4
                },
                new Course {
                    Name = "Web Development",
                    Description = "Full stack web development",
                    Credit = 3
                },
                new Course {
                    Name = "Machine Learning",
                    Description = "Introduction to ML algorithms",
                    Credit = 4
                }

            };

            courseRepository.AddRange(courses);
            courseRepository.Save();
        }

        public void CompleteCourse(int userId, int courseId)
        {
            var user = userRepository.Get(userId);
            var userCourse = _context.UserCourses
            .FirstOrDefault(uc => uc.UserId == userId && uc.CourseId == courseId);

            if (userCourse == null)
            {
                throw new ArgumentException("User or Course not found.");
            }

            userCourse.CompletionPercentage = 100;

            var skillsToAdd = _context.Courses
            .Where(c => c.CourseId == courseId)
            .SelectMany(c => c.Skills)
            .ToList();

            foreach (var skill in skillsToAdd)
            {
                user.Skills.Add(skill);
            }

            _context.SaveChanges();
        }

        public void CreateCourse(Course course)
        {
            courseRepository.Add(course);
        }

        public void DeleteCourse(Course course)
        {
            courseRepository.Delete(course);
        }

        public void DeleteCourseById(int id)
        {
            courseRepository.Delete(courseRepository.Get(id));
        }

        public void EnrollUserInCourse(int userId, int courseId)
        {
            var user = userRepository.Get(userId);
            var course = courseRepository.Get(courseId);

            if (user == null || course == null)
            {
                throw new ArgumentException("User or Course not found.");
            }

            var userCourse = new UserCourse { UserId = userId, CourseId = courseId, CompletionPercentage = 0 };
            _context.UserCourses.Add(userCourse);
            _context.SaveChanges();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return courseRepository.GetAll();
        }

        public Course GetCourseById(int id)
        {
            var course = courseRepository.Get(id);

            if (course == null)
            {
                throw new ArgumentException("Course not found.");
            }

            return course;
        }

        public Course GetCourseByName(string name)
        {
            var course = courseRepository.GetAll().FirstOrDefault(c => c.Name == name);
            if (course != null)
            {
                return course;
            }
            return null;
        }

        public void UpdateCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            courseRepository.Update(course);
        }

        public void UpdateUserCourse(UserCourse userCourse)
        {
            _context.UserCourses.Update(userCourse);
        }

        public List<Material> GetMaterialsNotInCourse(int courseId)
        {
            var materialsInCourse = _context.Courses
            .Where(cm => cm.CourseId == courseId)
            .SelectMany(c => c.Materials)
            .ToList();

            var materialsNotInCourse = _context.Materials
                .AsEnumerable()
                .Except(materialsInCourse)
                .ToList();

            return materialsNotInCourse;
        }

        public List<Skill> GetSkillsNotInCourse(int courseId)
        {
            var skillsInCourse = _context.Courses
            .Where(cs => cs.CourseId == courseId)
            .SelectMany(c => c.Skills)
            .ToList();

            var skillsNotInCourse = _context.Skills
                .AsEnumerable()
                .Except(skillsInCourse)
                .ToList();

            return skillsNotInCourse;
        }

        public List<int> GetEnrolledUsers(int courseId)
        {
            var enrolledUsers = _context.UserCourses
                .Where(uc => uc.CourseId == courseId)
                .Select(uc => uc.UserId)
                .ToList();

            return enrolledUsers;
        }
    }
}
