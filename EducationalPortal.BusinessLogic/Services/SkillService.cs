using EducationalPortal.BusinessLogic.Services.Interfaces;
using EducationalPortal.DataAccess.Repositories.Classes;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using EducationalPortal.DataAccess.Enums;
using EducationalPortal.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using EducationalPortal.DataAccess.Contexts;

namespace EducationalPortal.BusinessLogic.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository skillRepository;
        private readonly ICourseRepository courseRepository;
        private readonly EducationalPortalContext _context;

        public SkillService(EducationalPortalContext dbContext)
        {
            _context = dbContext;
            skillRepository = new SkillRepository(_context);
            courseRepository = new CourseRepository(_context);
        }

        public Skill GetSkillById(int skillId)
        {
            return skillRepository.Get(skillId);
        }

        public void FillSkillTable()
        {
            var skills = new List<Skill>
            {
                new Skill{ Name = "C# Programming", Level = SkillLevel.Beginner },
                new Skill{ Name = "C# Unity 3D Programming", Level = SkillLevel.Intermediate },
                new Skill{ Name = "ASP.NET Core", Level = SkillLevel.Advanced },
                new Skill{ Name = "Entity Framework", Level = SkillLevel.Beginner }
            };

            skillRepository.AddRange(skills);
            skillRepository.Save();
        }

        public void AddSkillToCourse(Skill skill, int courseId)
        {
            var course = courseRepository.Get(courseId);
            if(course != null)
            {
                course.Skills.Add(skill);
                courseRepository.Update(course);
            }
            else
            {
                throw new InvalidOperationException("Course not found");
            }
            _context.SaveChanges();
        }

        public void UpdateSkillLevel(int skillId, SkillLevel level)
        {
            var skillToUpdate = skillRepository.Get(skillId);
            if (skillToUpdate != null)
            {
                if (skillToUpdate.Level != level)
                {
                    if (skillToUpdate.Level > level)
                    {
                        throw new InvalidOperationException($"------> Skill cannot be downgraded from {skillToUpdate.Level} to {level}.");
                    }
                    else
                    {
                        skillToUpdate.Level = level;
                        skillRepository.Update(skillToUpdate);
                    }
                }
                else
                {
                    throw new InvalidOperationException($"------> Skill level is already at {skillToUpdate.Level}.");
                }
            }
            else
            {
                throw new InvalidOperationException("------> Skill not found.");
            }
        }

        public void UpdateSkillName(int skillId, string name)
        {
            var skillToUpdate = skillRepository.Get(skillId);
            if (skillToUpdate != null)
            {
                skillToUpdate.Name = name;
                skillRepository.Update(skillToUpdate);
            }
            else
            {
                throw new InvalidOperationException("------> Skill not found.");
            }
        }

        public void UpgradeSkillLevel(int skillId)
        {
            var skillToUpdate = skillRepository.Get(skillId);
            if (skillToUpdate != null)
            {
                if (skillToUpdate.Level < SkillLevel.Master) 
                {
                    skillToUpdate.Level++;
                    skillRepository.Update(skillToUpdate);
                }
                else
                {
                    throw new InvalidOperationException($"------> Skill is already at Max Level ({SkillLevel.Master}).");
                }
            }
            else
            {
                throw new InvalidOperationException("------> Skill not found.");
            }
        }

        public void RemoveSkillFromCourse(Skill skill, int courseId)
        {
            var course = courseRepository.Get(courseId);

            if (course != null)
            {
                course.Skills.Remove(skill);
                courseRepository.Update(course);
            }
            else
            {
                throw new InvalidOperationException("------> Skill or Course not found.");
            }
        }

        public void CreateSkill(Skill skill)
        {
            skillRepository.Add(skill);
        }

        public void DeleteSkill(int skillId)
        {
            var skill = skillRepository.Get(skillId);
            skillRepository.Delete(skill);
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            return skillRepository.GetAll();
        }

    }
}
