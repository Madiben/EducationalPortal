using EducationalPortal.BusinessLogic.Services.Interfaces;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EducationalPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IMaterialService _materialService;
        private readonly ISkillService _skillService;
        private readonly IAuthenticationService _authService;

        public HomeController(ILogger<HomeController> logger, ICourseService courseService, IUserService userService, IMaterialService materialService, ISkillService skillService, IAuthenticationService authService)
        {
            _logger = logger;
            _courseService = courseService;
            _userService = userService;
            _materialService=materialService;
            _skillService=skillService;
            _authService=authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //_courseService.FillCourseTable();
            //_materialService.FillMaterialTable();
            //_skillService.FillSkillTable();
            //_userService.FillUserTable();
            return View();
        }



        [HttpPost]
        public IActionResult Add(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }
            _courseService.CreateCourse(course);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}