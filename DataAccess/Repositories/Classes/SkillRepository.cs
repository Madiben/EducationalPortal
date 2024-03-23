using EducationalPortal.DataAccess.Contexts;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationalPortal.DataAccess.Repositories.Classes
{
    public class SkillRepository : ISkillRepository
    {
        protected readonly EducationalPortalContext dbContext;

        public SkillRepository(EducationalPortalContext context)
        {
            dbContext = context;
        }
        public void Add(Skill skill)
        {
            dbContext.Set<Skill>().Add(skill);
            Save();
        }

        public void AddRange(List<Skill> skills)
        {
            dbContext.Set<Skill>().AddRange(skills);
            Save();
        }

        public Skill Get(int id)
        {
            return dbContext.Set<Skill>().Find(id);
        }

        public IEnumerable<Skill> GetAll()
        {
            return dbContext.Set<Skill>().ToList();
        }

        public void Update(Skill skill)
        {
            dbContext.Entry(skill).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Delete(Skill skill)
        {
            dbContext.Set<Skill>().Remove(skill);
            Save();
        }
    }
}