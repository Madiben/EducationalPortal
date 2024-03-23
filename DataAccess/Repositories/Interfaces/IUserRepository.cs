using EducationalPortal.DataAccess.Models.Entities;

namespace EducationalPortal.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public void Add(User entity);

        public void AddRange(List<User> entities);

        public User Get(int id);

        public IEnumerable<User> GetAll();

        public void Update(User entity);

        public void Delete(User entity);

        public void Save();

        public User GetUserByUsername(string username);
    }
}
