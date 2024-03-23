using EducationalPortal.DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.BusinessLogic.Services.Interfaces
{
    public interface ICourseService
    {
        public void FillCourseTable();

        public void CreateCourse(Course course);

        public void UpdateCourse(Course course);

        public void DeleteCourse(Course course);

        public Course GetCourseById(int id);

        public Course GetCourseByName(string name);

        public IEnumerable<Course> GetAllCourses();

        public void DeleteCourseById(int id);

        public void CompleteCourse(int userId, int courseId);

        public void EnrollUserInCourse(int userId, int courseId);

        public void UpdateUserCourse(UserCourse userCourse);

        public List<Material> GetMaterialsNotInCourse(int courseId);

        public List<Skill> GetSkillsNotInCourse(int courseId);

        public List<int> GetEnrolledUsers(int courseId);
    }
}
