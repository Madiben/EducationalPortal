using EducationalPortal.DataAccess.Contexts;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationalPortal.DataAccess.Repositories.Classes
{
    public class MaterialRepository : IMaterialRepository
    {
        protected readonly EducationalPortalContext dbContext;

        public MaterialRepository(EducationalPortalContext context)
        {
            dbContext = context;
        }
        public void Add(Material material)
        {
            dbContext.Set<Material>().Add(material);
            Save();
        }

        public void AddRange(List<Material> materials)
        {
            dbContext.Set<Material>().AddRange(materials);
            Save();
        }

        public Material Get(int id)
        {
            return dbContext.Set<Material>().Find(id);
        }

        public IEnumerable<Material> GetAll()
        {
            return dbContext.Set<Material>().ToList();
        }

        public void Update(Material material)
        {
            try
            {
                dbContext.Entry(material).State = EntityState.Modified;
                Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflict here, e.g., log the conflict, refresh entity, etc.
                // You can access the current and original values from ex.Entries
                // and implement your conflict resolution strategy.
                throw;
            }
        }

        public void Save()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflict here, if necessary.
                throw;
            }
        }

        public void Delete(Material material)
        {
            dbContext.Set<Material>().Remove(material);
            Save();
        }
    }
}