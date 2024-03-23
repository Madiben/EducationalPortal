using EducationalPortal.DataAccess.Enums;
using EducationalPortal.DataAccess.Models.Entities;

namespace EducationalPortal.ViewModels
{
    public class ProfileEditModelView
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ProfileEditModelView() { }

        public ProfileEditModelView(int id, string username,string email,string fname,string lname)
        {
            Id = id;
            Username = username;
            Email = email;
            FirstName = fname;
            LastName = lname;
        }
    }
}
