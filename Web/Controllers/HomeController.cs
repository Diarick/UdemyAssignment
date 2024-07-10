using DataAccess.Data;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Diagnostics;

namespace Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ProductRepository _productRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UdemyAssignmentDBContext db)
        {
            _logger = logger;
            _productRepository = new ProductRepository(db);
        }

        public IActionResult Index()
        {
            List<Product> Products = _productRepository.GetAll().ToList();
            return View(Products);
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
