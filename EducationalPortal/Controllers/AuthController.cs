using EducationalPortal.BusinessLogic.Services.Interfaces;
using EducationalPortal.DataAccess.Enums;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Data;

namespace EducationalPortal.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;

        public AuthController(IUserService userService, IAuthenticationService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginModel loginModelView)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }

                return View(loginModelView);
            }

            var user = _authService.LoginUser(loginModelView.Username,loginModelView.Password);

            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(loginModelView);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Logout")]
        public ActionResult Logout()
        {
            _authService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterModel register)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(register);
            }

            var user = new User(register.Username, register.Password, register.Email, register.FirstName, register.LastName, register.Role);

            if (user == null)
            {
                return NotFound();
            }

            _authService.Register(user);
            _authService.LoginUser(user.Username,user.Password);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("ResetPassword/{id}")]
        public ActionResult ResetPassword(int id)
        {
            var user = _userService.GetUser(id);
            var resetPasswordModel = new ResetPasswordModelView(id,user.Username,user.Password);
            return View(resetPasswordModel);
        }

        [HttpPost("ResetPassword/{id}")]
        public ActionResult ResetPassword(int id,ResetPasswordModelView resetPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(resetPasswordModel);
            }

            var user = _userService.GetUser(id);

            _userService.UpdateUserPassword(id, user.Password, resetPasswordModel.Password);
   

            return RedirectToAction("Profile", "User", new {id=id});
        }
    }
}
