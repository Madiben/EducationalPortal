using System;
using EducationalPortal.DataAccess.Enums;

namespace EducationalPortal.DataAccess.Models.Entities
{
    public class Skill 
    {
        public int SkillId { get; set; }

        public string Name { get; set; }

        public SkillLevel Level { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Course> Courses { get; set; }

        public Skill()
        {
            Users = new HashSet<User>();
            Courses = new HashSet<Course>();
        }

        public Skill(int id, string name, SkillLevel level)
        {
            SkillId = id;
            Name = name;
            Level = level;
        }

        public override string ToString()
        {
            return $"Skill ID: {SkillId}, Name: {Name}, Level: {Level}";
        }
    }
}
