using EducationalPortal.BusinessLogic.Services.Interfaces;
using EducationalPortal.DataAccess.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPortal.Controllers
{
    [Route("[controller]")]
    public class MaterialsController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IMaterialService _materialService;
        private readonly ISkillService _skillService;

        public MaterialsController(ICourseService courseService, IMaterialService materialService, ISkillService skillService)
        {
            _courseService = courseService;
            _materialService = materialService;
            _skillService = skillService;
        }

        [HttpGet]
        public IActionResult Materials()
        {
            var materials = _materialService.GetMaterialList();

            return View(materials);
        }


        // GET: MaterialsController
        public ActionResult Index()
        {
            var materials = _materialService.GetMaterialList();

            return View(materials);
        }

        // GET: MaterialsController/Details/5
        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            var material = _materialService.GetMaterialById(id);
            return View(material);
        }

        // GET: MaterialsController/Create
        [HttpGet("Create")]
        public ActionResult Add()
        {
            return View();
        }

        // POST: MaterialsController/Create
        [HttpPost("Create/AddArticle")]
        public ActionResult AddArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(article);
            }
            Console.WriteLine($"\n\n\n\n Received Article: {article.Name}, {article.Description}, {article.Source}, {article.Author}, {article.PublicationDate}\n\n\n\n");
            _materialService.AddMaterial(article);

            return RedirectToAction("Index");
        }

        [HttpPost("Create/AddBook")]
        public ActionResult AddBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(book);
            }
            _materialService.AddMaterial(book);

            return RedirectToAction("Index");
        }

        [HttpPost("Create/AddVideo")]
        public ActionResult AddVideo(Video video)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(video);
            }
            _materialService.AddMaterial(video);

            return RedirectToAction("Index");
        }

        // GET: MaterialsController/Edit
        [HttpGet("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var material = _materialService.GetMaterialById(id);

            if (material == null)
            {
                return NotFound();
            }

            if (material.Type == DataAccess.Enums.MaterialType.Book)
            {
                return RedirectToAction(nameof(EditBook), new { id });
            }
            else if (material.Type == DataAccess.Enums.MaterialType.Video)
            {
                return RedirectToAction(nameof(EditVideo), new { id });
            }
            else if (material.Type == DataAccess.Enums.MaterialType.Article)
            {
                return RedirectToAction(nameof(EditArticle), new { id });
            }
            else
            {
                return BadRequest("Invalid material type");
            }
        }

        [HttpGet("EditArticle/{id}")]
        public ActionResult EditArticle(int id)
        {
            var article = (Article)_materialService.GetMaterialById(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost("EditArticle/{id}")]
        public ActionResult EditArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(article);
            }
            var currentarticle = (Article)_materialService.GetMaterialById(id);

            if (currentarticle == null)
            {
                return NotFound();
            }
            else
            {
                currentarticle.Name = article.Name;
                currentarticle.Description = article.Description;
                currentarticle.Source = article.Source;
                currentarticle.Author = article.Author;
                currentarticle.PublicationDate = article.PublicationDate;
            }
            _materialService.UpdateMaterial(currentarticle);

            return RedirectToAction("Index");
        }

        [HttpGet("EditBook/{id}")]
        public ActionResult EditBook(int id)
        {
            var book = (Book)_materialService.GetMaterialById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost("EditBook/{id}")]
        public ActionResult EditBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(book);
            }
            var currentbook = (Book)_materialService.GetMaterialById(id);

            if (currentbook == null)
            {
                return NotFound();
            }
            else
            {
                currentbook.Name = book.Name;
                currentbook.Description = book.Description;
                currentbook.Authors = book.Authors;
                currentbook.Pages = book.Pages;
                currentbook.Format = book.Format;
                currentbook.YearOfPublication = book.YearOfPublication;
            }
            _materialService.UpdateMaterial(currentbook);

            return RedirectToAction("Index");
        }

        [HttpGet("EditVideo/{id}")]
        public ActionResult EditVideo(int id)
        {
            var video = (Video)_materialService.GetMaterialById(id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        [HttpPost("EditVideo/{id}")]
        public ActionResult EditVideo(int id, Video video)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(video);
            }
            var currentvideo = (Video)_materialService.GetMaterialById(id);

            if (currentvideo == null)
            {
                return NotFound();
            }
            else
            {
                currentvideo.Name = video.Name;
                currentvideo.Description = video.Description;
                currentvideo.DurationInMinutes = video.DurationInMinutes;
                currentvideo.Quality = video.Quality;

            }
            _materialService.UpdateMaterial(currentvideo);
            return RedirectToAction("Index");
        }


        // GET: MaterialsController/Delete/5
        [HttpGet("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            _materialService.DeleteMaterial(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
