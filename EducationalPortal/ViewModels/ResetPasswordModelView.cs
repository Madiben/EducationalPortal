namespace EducationalPortal.ViewModels
{
    public class ResetPasswordModelView
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    
        public ResetPasswordModelView() { }

        public ResetPasswordModelView(int id, string username, string password)
        {
            Id=id;
            Username=username;
            Password=password;
        }
    }
    
}
