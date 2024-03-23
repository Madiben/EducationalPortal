using EducationalPortal.BusinessLogic.Services;
using EducationalPortal.BusinessLogic.Services.Interfaces;
using EducationalPortal.DataAccess.Models.Entities;
using EducationalPortal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPortal.Controllers
{
    [Route("[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IMaterialService _materialService;
        private readonly ISkillService _skillService;

        public CoursesController(ICourseService courseService, IUserService userService, IMaterialService materialService, ISkillService skillService)
        {
            _courseService = courseService;
            _userService=userService;
            _materialService=materialService;
            _skillService=skillService;
        }

        [HttpGet("Courses")]
        public IActionResult Courses()
        {
            var courses = _courseService.GetAllCourses();

            return View(courses);
        }

        // GET: CoursesController
        [HttpGet]
        public ActionResult Index()
        {
            var courses = _courseService.GetAllCourses();

            return View(courses);
        }

        // GET: CoursesController/5
        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            var course = _courseService.GetCourseById(id);
            return View(course);
        }

        // GET: CoursesController/Create
        [HttpGet("Create")]
        public ActionResult Add()
        {
            return View();
        }

        // POST: CoursesController/Create
        [HttpPost("Create")]
        public IActionResult Add(Course course)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(course);
            }
            _courseService.CreateCourse(course);
            return RedirectToAction(nameof(AddMaterialAndSkill),new{ id = course.CourseId});
        }

        // GET: CoursesController/Edit/5
        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var course = _courseService.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: CoursesController/Edit/5
        [HttpPost("Edit/{id}")]
        public ActionResult Edit(int id, Course course)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(course);
            }

            var currentCourse = _courseService.GetCourseById(id);

            if (currentCourse == null)
            {
                return NotFound();
            }
            else
            {
                currentCourse.Name = course.Name;
                currentCourse.Description = course.Description;
                currentCourse.Credit = course.Credit;
            }

            _courseService.UpdateCourse(currentCourse);
            return RedirectToAction(nameof(AddMaterialAndSkill), new { id = id });
        }

        // GET: CoursesController/Delete/5
        [HttpGet("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            _courseService.DeleteCourseById(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("AddMaterialAndSkill/{id}")]
        public ActionResult AddMaterialAndSkill(int id)
        {
            var materials = _courseService.GetMaterialsNotInCourse(id);
            var skills = _courseService.GetSkillsNotInCourse(id);
            var model = new AddMaterialAndSkillModelView(id, materials, skills);
            return View(model);
        }

        [HttpGet("AddMaterial/{courseId}/{materialId}")]
        public ActionResult AddMaterial(int courseId, int materialId)
        {
            return View();
        }

        [HttpPost("AddMaterial/{courseId}/{materialId}")]
        public ActionResult AddMaterialPost(int courseId, int materialId)
        {
            var material = _materialService.GetMaterialById(materialId);
            _materialService.AddMaterialToCourse(material, courseId);

            var enrolledUsers = _courseService.GetEnrolledUsers(courseId);
            foreach (var userId in enrolledUsers)
            {
                _materialService.EnrollUserInMaterial(userId, materialId);
            }

            return RedirectToAction(nameof(AddMaterialAndSkill), new { id = courseId });
        }

        [HttpGet("AddSkill/{courseId}/{skillId}")]
        public ActionResult AddSkill(int courseId, int skillId)
        {
            return View();
        }

        [HttpPost("AddSkill/{courseId}/{skillId}")]
        public ActionResult AddSkillPost(int courseId, int skillId)
        {
            var skill = _skillService.GetSkillById(skillId);
            _skillService.AddSkillToCourse(skill, courseId);

            return RedirectToAction(nameof(AddMaterialAndSkill), new { id = courseId });
        }


  
    }
}
