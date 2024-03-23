using EducationalPortal.BusinessLogic.Services.Interfaces;
using EducationalPortal.DataAccess.Repositories.Classes;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using EducationalPortal.DataAccess.Enums;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.DataAccess.Contexts;

namespace EducationalPortal.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly EducationalPortalContext _context;

        public UserService(EducationalPortalContext dbContext)
        {
            _context = dbContext;
            userRepository = new UserRepository(_context);
        }
        public void FillUserTable()
        {
            var users = new List<User>
            {
                new User
                {
                    Username = "mahdi",
                    Email = "mahdi@example.com",
                    FirstName = "mahdi",
                    LastName = "Bentaleb",
                    Password = HashPassword("password123"),
                    Role = Role.Admin
                },
                
            };
            for (int i = 1; i <= 10; i++)
            {
                users.Add(new User
                {
                    Username = $"user{i}",
                    Password = HashPassword($"password{i}"), 
                    Email = $"user{i}@example.com",
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    Role =Role.Student

                });
            }
            userRepository.AddRange(users);
            userRepository.Save();
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public void UpdateUserPassword(int userId, string oldPassword,string newPassword)
        {
            var userToUpdate = userRepository.Get(userId);
            if (VerifyPassword(oldPassword, userToUpdate.Password)) {
                userToUpdate.Password = HashPassword(newPassword);
                userRepository.Update(userToUpdate);
                Console.WriteLine("-----> Password Updated Succefully <-----");
            }
            else
            {
                Console.WriteLine("-----> Wrong Password Update Failed <-----");
            }
        }

        public void DeleteUser(int id)
        {
            userRepository.Delete(GetUser(id));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userRepository.GetAll();
        }

        private bool CheckUsername(string username)
        {
            var user = GetAllUsers().FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                return false;
            }
            return true;
        }

        private bool CheckEmail(string email)
        {
            var user = userRepository.GetAll().FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return false;
            }
            return true;
        }

        public User GetUserByUsername(string username)
        {
            var users = GetAllUsers();
            foreach(var user in users)
            {
                if(user.Username == username)
                {
                    return (User)user;
                }
            }
            return null;
        }

        public User GetUser(int id)
        {
            return userRepository.Get(id);
        }

        public User GetUserWithData(int id)
        {
            return _context.Users
                    .Include(u => u.UserCourses)
                    .ThenInclude(uc => uc.Course)
                    .Include(u => u.UserMaterials)
                    .ThenInclude(um => um.Material)
                    .FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }

        public void UpdateUserEmail(int userId, string email)
        {
            var user = userRepository.Get(userId);
            bool isEmailValid = false;

            if (user != null)
            {
                isEmailValid = CheckEmail(email);
                if (!isEmailValid)
                {
                    Console.WriteLine("Email already in use. Please enter a different email.");
                }
                else
                {
                    user.Email = email;
                    userRepository.Update(user);
                    Console.WriteLine($"Username updated Succefully. ==> {email}");
                }
            }
            else
            {
                throw new InvalidOperationException("------> User not found. <------");
            }
        }

        public void UpdateUserFirstName(int userId, string firstName)
        {
            var user = userRepository.Get(userId);

            if (user != null)
            {
                user.FirstName = firstName;
                userRepository.Update(user);
            }
            else
            {
                throw new InvalidOperationException("------> User not found. <------");
            }
        }

        public void UpdateUserLastName(int userId, string lastName)
        {
            var user = userRepository.Get(userId);

            if (user != null)
            {
                user.LastName = lastName;
                userRepository.Update(user);
            }
            else
            {
                throw new InvalidOperationException("------> User not found. <------");
            }
        }

        public void UpdateUsername(int userId, string username)
        {
            var user = userRepository.Get(userId);
            bool isUsernameValid = false;
            if (user != null)
            {
                isUsernameValid = CheckUsername(username);

                if (!isUsernameValid)
                {
                    Console.WriteLine("Username already exists. Pick another one");
                }
                else
                {
                    user.Username = username;
                    userRepository.Update(user);
                    Console.WriteLine($"Username updated Succefully. ==> {username}");
                }
            }
            else
            {
                throw new InvalidOperationException("------> User not found. <------");
            }
        }
        
        public List<Course> GetUserEnrolledCourses(int id)
        {
            var user = _context.Users
                .Include(u => u.UserCourses)
                .ThenInclude(uc => uc.Course)
                .FirstOrDefault(u => u.Id == id);
            
            if(user == null)
            {
                return new List<Course>();
            }

            var enrolledCourses = user.UserCourses.Select(uc => uc.Course).ToList();

            return enrolledCourses;
        }

        public List<Course> GetUserUnEnrolledCourses(int userId)
        {
            var enrolledCourseIds = GetUserEnrolledCourses(userId).Select(c => c.CourseId).ToList();

            var unEnrolledCourses = _context.Courses
                .Where(c => !enrolledCourseIds.Contains(c.CourseId))
                .ToList();

            return unEnrolledCourses;
        }

        public List<Material> GetUserEnrolledMaterials(int id)
        {
            var user = _context.Users
                .Include(u => u.UserMaterials)
                .ThenInclude(uc => uc.Material)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return new List<Material>();
            }

            var enrolledMaterials = user.UserMaterials.Select(um => um.Material).ToList();

            return enrolledMaterials;
        }

        public List<UserCourse> GetUserCoursesDetails(int userId)
        {
            return _context.UserCourses
                .Where(uc => uc.UserId == userId)
                .Include(c=>c.Course)
                .ToList();
        }

        public List<UserMaterial> GetUserMaterialsDetails(int userId)
        {
            return _context.UserMaterials
                .Where(um => um.UserId == userId)
                .Include(m=>m.Material)
                .ToList();
        }

        public void DropCourse(int userId, int courseId)
        {
            var userCourse = _context.UserCourses
                .FirstOrDefault(uc => uc.UserId == userId && uc.CourseId == courseId);

            if (userCourse != null)
            {
                _context.UserCourses.Remove(userCourse);
                _context.SaveChanges();
            }
        }
    }
}
