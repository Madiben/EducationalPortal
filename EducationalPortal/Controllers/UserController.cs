using EducationalPortal.BusinessLogic.Services.Interfaces;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IMaterialService _materialService;
        private readonly ISkillService _skillService;
        public UserController(ILogger<HomeController> logger, ICourseService courseService, IUserService userService, IMaterialService materialService, ISkillService skillService)
        {
            _logger = logger;
            _courseService = courseService;
            _userService = userService;
            _materialService=materialService;
            _skillService=skillService;
        }

        [HttpGet("Profile/{id}")]
        public IActionResult Profile(int id)
        {
            var user = _userService.GetUserWithData(id);
            return View(user);
        }

        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var user = _userService.GetUser(id);
            var profile = new ProfileEditModelView(id,user.Username,user.Email,user.FirstName,user.LastName);
            if (user == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: CoursesController/Edit/5
        [HttpPost("Edit/{id}")]
        public ActionResult Edit(int id, ProfileEditModelView profileEditView)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(profileEditView);
            }

            _userService.UpdateUserEmail(id, profileEditView.Email);
            _userService.UpdateUsername(id, profileEditView.Username);
            _userService.UpdateUserFirstName(id, profileEditView.FirstName);
            _userService.UpdateUserLastName(id, profileEditView.LastName);

            return RedirectToAction("Profile", new { id = id });
        }

        [HttpGet("{id}/JoinCourse")]
        public ActionResult JoinCourse(int id)
        {
            var courses = _userService.GetUserUnEnrolledCourses(id);
            
            return View(courses);
        }

        [HttpGet("{id}/MyCourses")]
        public ActionResult MyCourses(int id)
        {
            var courses = _userService.GetUserCoursesDetails(id);
            var material = _userService.GetUserMaterialsDetails(id);
            
            /*foreach(var course in courses)
            {
                _materialService.UpdateCourseCompelationPercentage(id, course.CourseId);
            }*/

            var mycoursesModel = new MyCoursesModelView(id,courses,material);
            return View(mycoursesModel);
        }

        [HttpGet("{userId}/EnrollCourse/{courseId}")]
        public ActionResult EnrollCourse(int userId,int courseId) 
        {
            try
            {
                _courseService.EnrollUserInCourse(userId, courseId);

                return RedirectToAction("Profile", new { id = userId });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("{userId}/StudyCourse/{courseId}")]
        public ActionResult StudyCourse(int userId, int courseId)
        {
            try
            {
                _courseService.CompleteCourse(userId, courseId);

                return RedirectToAction("Profile", new { id = userId });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet("{userId}/StudyMaterial/{materialId}")]
        public ActionResult StudyMaterial(int userId, int materialId)
        {
            try
            {
                _materialService.CompleteMaterial(userId, materialId);

                return RedirectToAction("Profile", new { id = userId });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("DropCourse/{userId}/{courseId}")]
        public IActionResult DropCourse(int userId, int courseId)
        {
            var course = _courseService.GetCourseById(courseId);
            try
            {
                _userService.DropCourse(userId, courseId);
                foreach(var material in course.Materials)
                {
                    _materialService.DropMaterialsForUser(userId, material.MaterialId);
                }

                return RedirectToAction("Profile", new { id = userId });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
