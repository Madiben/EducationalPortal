using EducationalPortal.DataAccess.Contexts;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationalPortal.DataAccess.Repositories.Classes
{
    public class UserRepository : IUserRepository
    {
        protected readonly EducationalPortalContext dbContext;

        public UserRepository(EducationalPortalContext context)
        {
            dbContext = context;
        }
        public void Add(User user)
        {
            dbContext.Set<User>().Add(user);
            Save();
        }

        public void AddRange(List<User> users)
        {
            dbContext.Set<User>().AddRange(users);
            Save();
        }

        public User Get(int id)
        {
            return dbContext.Set<User>().Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return dbContext.Set<User>().ToList();
        }

        public void Update(User user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Delete(User user)
        {
            dbContext.Set<User>().Remove(user);
            Save();
        }

        public User GetUserByUsername(string username)
        {
            return dbContext.Set<User>().FirstOrDefault(u => u.Username == username);
        }

    }
}
