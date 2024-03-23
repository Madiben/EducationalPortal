using EducationalPortal.DataAccess.Repositories.Interfaces;
using EducationalPortal.DataAccess.Models.Entities;

namespace EducationalPortal.DataAccess.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        public Course GetByName(string name);

        public void Add(Course course);

        public void AddRange(List<Course> courses);

        public Course Get(int id);

        public IEnumerable<Course> GetAll();

        public void Update(Course course);

        public void Delete(Course course);

        public void Save();

    }
}
