using EducationalPortal.DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public void FillUserTable();


        public void UpdateUser(User user);

        public User GetUser(int id);

        public void DeleteUser(int id);

        public IEnumerable<User> GetAllUsers();

        public void UpdateUserPassword(int userId, string oldPassword, string newPassword);

        public void UpdateUsername(int userId, string username);

        public void UpdateUserEmail(int userId, string email);

        public void UpdateUserFirstName(int userId, string firstName);

        public void UpdateUserLastName(int userId, string lastName);

        public bool VerifyPassword(string password, string hashedPassword);

        public string HashPassword(string password);

        public List<Course> GetUserEnrolledCourses(int userId);

        public List<Course> GetUserUnEnrolledCourses(int userId);

        public User GetUserWithData(int id);

        public List<Material> GetUserEnrolledMaterials(int id);

        public List<UserCourse> GetUserCoursesDetails(int userId);

        public List<UserMaterial> GetUserMaterialsDetails(int userId);

        public void DropCourse(int userId, int courseId);
    }
}
