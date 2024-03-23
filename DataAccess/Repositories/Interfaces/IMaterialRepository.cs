using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.DataAccess.Repositories.Interfaces
{
    public interface IMaterialRepository
    {
        public void Add(Material material);

        public void AddRange(List<Material> materials);

        public Material Get(int id);

        public IEnumerable<Material> GetAll();

        public void Update(Material material);

        public void Delete(Material material);

        public void Save();

    }
}

