namespace EducationalPortal.DataAccess.Models.Entities
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Credit { get; set; }

        public ICollection<Material> Materials { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; }

        public Course()
        {
            Materials = new HashSet<Material>();
            Skills = new HashSet<Skill>();
            UserCourses = new HashSet<UserCourse>();
        }

        public Course(int id, string name, string description, int credit)
        {
            CourseId = id;
            Name = name;
            Description = description;
            Credit = credit;
            Materials = new HashSet<Material>(); ;
            Skills = new HashSet<Skill>(); ;
            UserCourses = new HashSet<UserCourse>();
        }

        public Course(int id, string name, string description, int credit, ICollection<Material> materials, ICollection<Skill> skills)
        {
            CourseId = id;
            Name = name;
            Description = description;
            Credit = credit;
            Materials = materials;
            Skills = skills;
            UserCourses = new HashSet<UserCourse>();
        }

        public override string ToString()
        {
            var materialsString = string.Join(", ", Materials.Select(m => m.Name));
            var skillsString = string.Join(", ", Skills.Select(s => s.Name));
            return $"Course ID: {CourseId}, Name: {Name},\n Description: {Description}, Credit: {Credit},\n Materials: [{materialsString}],\n Skills: [{skillsString}]";

        }
    }
}
