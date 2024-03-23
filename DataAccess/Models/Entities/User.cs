using System;
using EducationalPortal.DataAccess.Enums;

namespace EducationalPortal.DataAccess.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; }

        public ICollection<UserMaterial> UserMaterials { get; set; }

        public User()
        {
            UserMaterials = new HashSet<UserMaterial>();
            Skills = new HashSet<Skill>();
            UserCourses = new HashSet<UserCourse>();
        }

        public User(string username, string password, string email, string firstName, string lastName, Role role)
        {
            Username = username;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            UserMaterials = new HashSet<UserMaterial>();
            Skills = new HashSet<Skill>();
            UserCourses = new HashSet<UserCourse>();
        }

        public override string ToString()
        {
            var coursesString = string.Join(", ", UserCourses.Select(c => c.ToString()));
            var skillsString = string.Join(", ", Skills.Select(s => s.ToString()));
            var materialsString = string.Join(", ", UserMaterials.Select(m => m.ToString()));
            return $" User ID: {Id}, Username: {Username}, Email: {Email},\n First Name: {FirstName}, Last Name: {LastName},\n Skills: [{skillsString}],\n Courses: [{coursesString}],\n Materials: [{materialsString}]";
        }
    }
}
