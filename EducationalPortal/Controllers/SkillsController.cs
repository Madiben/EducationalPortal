using EducationalPortal.BusinessLogic.Services.Interfaces;
using EducationalPortal.DataAccess.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPortal.Controllers
{
    [Route("[controller]")]
    public class SkillsController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IMaterialService _materialService;
        private readonly ISkillService _skillService;

        public SkillsController(ICourseService courseService,IMaterialService materialService, ISkillService skillService)
        {
            _courseService = courseService;
            _materialService = materialService;
            _skillService = skillService;
        }

        [HttpGet]
        public IActionResult Skills()
        {
            var skills = _skillService.GetAllSkills();

            return View(skills);
        }
       


        // GET: SkillsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SkillsController/Details/5
        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            var skill = _skillService.GetSkillById(id);
            return View(skill);
        }

        // GET: SkillsController/Create
        [HttpGet("Create")]
        public ActionResult Add()
        {
            return View();
        }

        // POST: SkillsController/Create
        [HttpPost("Create")]
        public IActionResult Add(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(skill);
            }
            _skillService.CreateSkill(skill);
            return RedirectToAction(nameof(Index));

        }

        // GET: SkillsController/Edit/5
        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var skill = _skillService.GetSkillById(id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: SkillsController/Edit/5
        [HttpPost("Edit/{id}")]
        public ActionResult Edit(int id, Skill skill)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(skill);
            }
            _skillService.UpdateSkillName(id, skill.Name);
            return RedirectToAction(nameof(Index));
        }

        // GET: SkillsController/Delete/5
        [HttpGet("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            _skillService.DeleteSkill(id);
            return RedirectToAction(nameof(Index));
        }

    }

}
