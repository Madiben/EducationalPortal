using EducationalPortal.DataAccess.Contexts;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.DataAccess.Repositories.Classes
{
    public class CourseRepository : ICourseRepository
    {
        protected readonly EducationalPortalContext dbContext;

        public CourseRepository(EducationalPortalContext context)
        {
            dbContext = context;
        }
        public void Add(Course course)
        {
            dbContext.Set<Course>().Add(course);
            Save();
        }

        public void AddRange(List<Course> courses)
        {
            dbContext.Set<Course>().AddRange(courses);
            Save();
        }

        public Course Get(int id)
        {
            return dbContext.Set<Course>().Find(id);
        }

        public IEnumerable<Course> GetAll()
        {
            return dbContext.Set<Course>().ToList();
        }

        public void Update(Course course)
        {
            dbContext.Entry(course).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Delete(Course course)
        {
            dbContext.Set<Course>().Remove(course);
            Save();
        }

        Course ICourseRepository.GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}