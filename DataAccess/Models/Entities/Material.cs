using EducationalPortal.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace EducationalPortal.DataAccess.Models.Entities
{
    public abstract class Material
    {
        public int MaterialId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<UserMaterial> UserMaterials { get; set; } = new List<UserMaterial>();

        public MaterialType Type { get; set; }

        protected Material(MaterialType type)
        {
            Type = type;
            Courses = new HashSet<Course>();
        }

        public override string ToString()
        {
            return $"Material: ID: {MaterialId}, Name: {Name}, Description: {Description}, Type: {Type}";
        }
    }
}
