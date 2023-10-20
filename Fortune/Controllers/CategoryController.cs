using Microsoft.AspNetCore.Mvc;
using Fortune.Data;
using Fortune.Models;

namespace Fortune.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;   
        public CategoryController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            List<Category> Category = _dbContext.Category.ToList();
            return View(Category);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category Category)
        {
            _dbContext.Category.Add(Category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
