using Fortunes.DataAccess.Repository.IRepository;
using Fortunes.Models;
using Microsoft.AspNetCore.Mvc;
using Fortunes.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Fortunes.Utility;

namespace Fortune.Controllers
{
    [Authorize]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _WebHostEnvironment;   
        public ProductsController(IUnitOfWork UnitOfWork, IWebHostEnvironment WebHostEnvironment)
        {
                this._UnitOfWork = UnitOfWork;   
                this._WebHostEnvironment = WebHostEnvironment;              
        }
        public IActionResult Index()
        {
            try
            {
                List<Product> products = _UnitOfWork.products.GetAll().ToList();
                
                for(int i = 0; i < products.Count; i++)
                {
                    products[i].category = _UnitOfWork.Category.Get(x => x.Id == products[i].CategoryId);
                }
                return View(products);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }

        public IActionResult Upsert(int? id )
        {
            try
            {
                ProductVM obj = new()
                {
                    Categorylist = _UnitOfWork.Category
                .GetAll().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }),
                    product = new Product()
                };
                if(id == null || id == 0)
                {
                    return View(obj);
                }
                else
                {
                    obj.product = _UnitOfWork.products.Get(x => x.Id == id);
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            try
            {
                string wwwrootpath = _WebHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productpath = Path.Combine(wwwrootpath, @"Images\Products");

                    if (!string.IsNullOrEmpty(obj.product.ImageUrl))
                    {
                        var imgToBeDeleted = Path.Combine(wwwrootpath,obj.product.ImageUrl);

                        if (System.IO.File.Exists(imgToBeDeleted))
                        {
                            System.IO.File.Delete(imgToBeDeleted);
                        }
                    }
                    using(var filestream = new FileStream(Path.Combine(productpath,filename),FileMode.Create))
                    {
                        file.CopyTo(filestream);    
                    }
                    obj.product.ImageUrl = @"Images\Products\" + filename;
                }
                if(obj.product.Id == null || obj.product.Id == 0)
                {
                    _UnitOfWork.products.Add(obj.product);
                    _UnitOfWork.save();
                    return RedirectToAction("Index");
                }
                else
                {
                    _UnitOfWork.products.update(obj.product);
                    _UnitOfWork.save(); 
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }       
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var products = _UnitOfWork.products.Get(x => x.Id == id);
        //        return View(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = ex.Message; 
        //        return View();  
        //    }
           
        //}
        //[HttpPost]
        //public IActionResult Delete(int? id)
        //{
        //    try
        //    {
        //        if (id == null)
        //        {
        //            TempData["Error"] = "The Record has no data!";
        //        }
        //        var product = _UnitOfWork.products.Get(x => x.Id == id);
        //        _UnitOfWork.products.Delete(product);
        //        _UnitOfWork.save();
        //        TempData["Success"] = "Record Deleted Successfully !";
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = ex.Message; 
        //        return View();  
        //    }
           
        //}


        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = _UnitOfWork.products.GetAll().ToList();    

            for(var i = 0; i < products.Count; i++)
            {
                products[i].category = _UnitOfWork.Category.Get(x => x.Id == products[i].CategoryId);
            }
            return Json(new {data = products});  
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var datatodelete = _UnitOfWork.products.Get(x => x.Id == id);
                if (datatodelete == null)
                {
                    return Json(new { success = false, message = "Unable to delete !" });
                }

                if (!string.IsNullOrEmpty(datatodelete.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_WebHostEnvironment.WebRootPath, datatodelete.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _UnitOfWork.products.Delete(datatodelete);
                _UnitOfWork.save();

                return Json(new { success = true, massage = "Record Deleted Successfully !" });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();   
            }           
        }
        #endregion
    }
}
