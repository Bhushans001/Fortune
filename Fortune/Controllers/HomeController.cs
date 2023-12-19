using Fortunes.DataAccess;
using Fortunes.DataAccess.Repository.IRepository;
using Fortunes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fortune.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork;   

        public HomeController(ILogger<HomeController> logger, IUnitOfWork UnitOfWork)
        {
            _logger = logger;
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            var Products = _UnitOfWork.products.GetAll().ToList();
            return View(Products);
        }

        public IActionResult Details(int id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            var product = _UnitOfWork.products.Get(x => x.Id == id);
            product.category = _UnitOfWork.Category.Get(x => x.Id == product.CategoryId);
            return View(product);
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
