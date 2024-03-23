using EducationalPortal.DataAccess.Enums;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.BusinessLogic.Services.Interfaces
{
    public interface ISkillService
    {
        public void FillSkillTable();

        public Skill GetSkillById(int skillId);

        public void CreateSkill(Skill skill);

        public void AddSkillToCourse(Skill skill, int courseId);

        public void RemoveSkillFromCourse(Skill skill, int courseId);

        public void UpdateSkillLevel(int skillId, SkillLevel level);

        public void UpdateSkillName(int skillId, string name);

        public void UpgradeSkillLevel(int skillId);

        public void DeleteSkill(int skillId);

        public IEnumerable<Skill> GetAllSkills();
    }
}
