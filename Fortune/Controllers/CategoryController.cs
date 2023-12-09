using Microsoft.AspNetCore.Mvc;
using Fortunes.Models;
using Fortunes.DataAccess;
using Fortunes.DataAccess.Repository.IRepository;
using NuGet.Protocol.Core.Types;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Fortune.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _dbContext;
        public CategoryController(IUnitOfWork dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            try
            {
                var Category = _dbContext.Category.GetAll();
                return View(Category);
            }
            catch (Exception ex)
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
                var Name = (from obj in _dbContext.Category.GetAll()
                            where obj.Name == Category.Name
                            select obj.Name).FirstOrDefault();

                //if (Name != null)
                //{
                //    TempData["Error"] = "Category Name already Exists !";
                //    return View();
                //}

                _dbContext.Category.Add(Category);
                _dbContext.save();

                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }
        public IActionResult Edit(int? Id)
        {
            try
            {
                if (Id == null || Id == 0)
                {
                    TempData["Error"] = "Please check again something is Wrong";
                    return View();
                }
                Category? category = _dbContext.Category.Get(x => x.Id == Id);
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
                var Name = _dbContext.Category.Get(x => x.Id != category.Id && x.Name == category.Name);
                if (Name != null)
                {
                    TempData["Error"] = "Category Name Already Exists !";
                    return View();
                }
                if (ModelState.IsValid)
                {
                    _dbContext.Category.update(category);
                    _dbContext.save();
                    TempData["success"] = "Record(s) updated Successfully";
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
            try
            {
                if (Id == null || Id == 0)
                {
                    return NotFound();
                }
                Category? category = _dbContext.Category.Get(x => x.Id == Id);
                if (category == null)
                {
                    TempData["Error"] = "There is no data to delete";
                    return View();
                }

                return View(category);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            try
            {
                Category? category = _dbContext.Category.Get(x => x.Id == Id);
                _dbContext.Category.Delete(category);
                _dbContext.save();
                TempData["success"] = " Category Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.InnerException.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var Category = _dbContext.Category.GetAll();
                return Json(new { data = Category });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();  
            }
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var RecordTodelete = _dbContext.Category.Get(x => x.Id == id);

                if(RecordTodelete == null)
                {
                    return Json(new { success = false, Message = "Unable to delete !" });
                }

                _dbContext.Category.Delete(RecordTodelete);
                _dbContext.save();
                return Json(new { success = true, Message = "Record Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }
    }
}
