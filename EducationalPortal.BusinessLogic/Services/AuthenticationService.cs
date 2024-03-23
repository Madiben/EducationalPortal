using EducationalPortal.BusinessLogic.Services.Interfaces;
using EducationalPortal.DataAccess.Contexts;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.DataAccess.Repositories.Classes;
using EducationalPortal.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace EducationalPortal.BusinessLogic.Services
{
    public class AuthenticationService : Interfaces.IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly EducationalPortalContext _context;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor; 


        public AuthenticationService(EducationalPortalContext dbContext, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _context = dbContext;
            _userRepository = new UserRepository(_context);
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public User LoginUser(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);

            if (user != null && _userService.VerifyPassword(password, user.Password))
            {
                var login = new Login(user.Id, username);
                _context.Logins.Add(login);
                _context.SaveChanges();
                // Create claims for the authenticated user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(claims, "login");

                _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)).Wait();

                return user;
            }
            return null;
        }

        public bool CheckUsername(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                return false;
            }
            return true;
        }

        public bool CheckEmail(string email)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return false;
            }
            return true;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void Register(User user)
        {
            user.Password = HashPassword(user.Password);
            _userRepository.Add(user);
        }

        public void ChangePassword(int userId, string password)
        {
            var userToUpdate = _userRepository.Get(userId);
            userToUpdate.Password = _userService.HashPassword(password);
            _userRepository.Update(userToUpdate);
        }

        public int ResetPassword(string email)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email);
            return user.Id;
        }

        public void Logout()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        }
    }
}
