using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.DataAccess.Models.Entities
{
    public class Login
    {
        public Guid loginId { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public DateTime time { get; set; }
        public Login()
        {
            loginId = Guid.NewGuid();
        }

        public Login(int userId, string username)
        {
            loginId = Guid.NewGuid();
            UserId = userId;
            Username = username;
            time = DateTime.Now;
        }
    }
}
