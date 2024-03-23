using EducationalPortal.DataAccess.Models.Entities;

namespace EducationalPortal.ViewModels
{
    public class MyCoursesModelView
    {
        public int Id { get; set; }

        public List<UserCourse> Courses { get; set; }

        public List<UserMaterial> Materials { get; set; }

        public MyCoursesModelView() { }

        public MyCoursesModelView(int id, List<UserCourse> courses, List<UserMaterial> materials)
        {
            Id=id;
            Courses=courses;
            Materials=materials;
        }
    }
}
