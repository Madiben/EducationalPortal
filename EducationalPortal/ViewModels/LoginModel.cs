namespace EducationalPortal.ViewModels
{
    public class LoginModel
    {
        int id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public LoginModel()
        {
        }
        public LoginModel(string username, string password) {
            Username = username;
            Password = password;
        }
    }
}
