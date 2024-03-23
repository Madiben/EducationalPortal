using EducationalPortal.DataAccess.Models.Entities;

namespace EducationalPortal.DataAccess.Repositories.Interfaces
{
    public interface ISkillRepository
    {
        public void Add(Skill skill);

        public void AddRange(List<Skill> skills);

        public Skill Get(int id);

        public IEnumerable<Skill> GetAll();

        public void Update(Skill skill);

        public void Delete(Skill skill);

        public void Save();

    }
}
