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
            try
            {
                List<Category> Category = _dbContext.Category.ToList();
                return View(Category);
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category Category)
        {
            try
            {
                var Name = (from obj in _dbContext.Category
                           where obj.Name == Category.Name  
                           select obj.Name).FirstOrDefault();   

                if (Name != null)
                {
                    TempData["Error"] = "Category Name already Exists !";
                    return View();
                }

                _dbContext.Category.Add(Category);
                _dbContext.SaveChanges();

                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();  
            }           
        }
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            try
            {
                if (Id == null || Id == 0)
                {
                    TempData["Error"] = "Please check again something is Wrong";
                    return View();
                }
                Category? category = _dbContext.Category.FirstOrDefault(x => x.Id == Id);
                if (category == null)
                {
                    TempData["Error"] = "Please check again something is Wrong";
                    return View();
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();  
            }
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            try
            {
                var Name = (from oj in _dbContext.Category
                                  where oj.Name == category.Name
                                  && oj.Id != category.Id
                                  select oj.Name).FirstOrDefault();

                if (Name != null)
                {
                        TempData["Error"] = "Category Name Already Exists !";
                        return View();
                }
                if (ModelState.IsValid)
                {
                    _dbContext.Category.Update(category);
                    var count =  _dbContext.SaveChanges();
                    TempData["success"] = count + " Record(s) updated Successfully";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }           
        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? category = _dbContext.Category.FirstOrDefault(x => x.Id == Id);
            if (category == null)
            {
                TempData["Error"] = "There is no data to delete";
                return View();
            }

            return View(category);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            Category? category = _dbContext.Category.Find(Id);
            _dbContext.Category.Remove(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
