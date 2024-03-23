using EducationalPortal.DataAccess.Models.Entities;

namespace EducationalPortal.ViewModels
{
    public class AddMaterialAndSkillModelView
    {
        public int Id { get; set; }

        public List<Material> Materials { get; set; }

        public List<Skill> Skills { get; set; }

        public AddMaterialAndSkillModelView() { }

        public AddMaterialAndSkillModelView(int id, List<Material> materials, List<Skill> skills)
        {
            Id=id;
            Materials=materials;
            Skills=skills;
        }
    }
}
