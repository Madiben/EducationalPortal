using EducationalPortal.DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.BusinessLogic.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public User LoginUser(string username, string password);

        public void Logout();

        public bool CheckUsername(string username);

        public bool CheckEmail(string email);

        public void Register(User user);

        public void ChangePassword(int userId, string password);

        public int ResetPassword(string email);
    }
}
