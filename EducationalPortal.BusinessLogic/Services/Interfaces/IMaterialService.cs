using EducationalPortal.DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.BusinessLogic.Services.Interfaces
{
    public interface IMaterialService
    {
        public void FillMaterialTable();

        public void AddMaterial(Material material);

        public void AddMaterialToCourse(Material material, int courseId);

        public void RemoveMaterialFromCourse(Material material, int courseId);

        public void UpdateMaterial(Material material);

        public void DeleteMaterial(int materialId);

        public IEnumerable<Material> GetMaterialList();

        public Material GetMaterialById(int materialId);

        public void EnrollUserInMaterial(int userId, int materialId);

        public void UpdateUserMaterial(UserMaterial userMaterial);

        public void CompleteMaterial(int userId, int materialId);

        public void UpdateCourseCompelationPercentage(int userId, int courseId);

        public void DropMaterialsForUser(int userId, int materialId);
    }
}
