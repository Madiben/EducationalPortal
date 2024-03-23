
using EducationalPortal.DataAccess.Enums;
using EducationalPortal.DataAccess.Models.Entities;

namespace EducationalPortal.ViewModels
{
    public class RegisterModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public RegisterModel()
        {
        }

        public RegisterModel(string username, string password,string confirmpassword, string email, string firstName, string lastName, Role role)
        {
            Username = username;
            Password = password;
            ConfirmPassword = confirmpassword;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

    }
}
