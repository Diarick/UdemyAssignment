using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using Model.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models.ViewModels;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        public ProductController(UdemyAssignmentDBContext db)
        {
            _productRepository = new ProductRepository(db);
            _categoryRepository = new CategoryRepository(db);
        }
        public IActionResult Index()
        {
            List<Product> ObjProducts= _productRepository.GetAll().ToList();
            foreach(Product O in ObjProducts)
            {
                O.Category = _categoryRepository.Get(u => u.Id == O.CategoryID);
            }
            return View(ObjProducts);
        }
        public IActionResult Create()
        {
            ProductViewModel product = new ProductViewModel()
            {
                categories = _categoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                product = new Product()
            };

            return View(product);
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel Obj)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(Obj.product);
                _productRepository.Save();

                TempData["success"] = "Product has been created!";
                return RedirectToAction("Index", "Product");
            }

            Obj.categories = _categoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(Obj);
        }
        public IActionResult Update(int? ProductId)
        {
            if (ProductId == 0 || ProductId == null)
            {
                return NotFound();
            }

            Product? obj = _productRepository.Get(c => c.Id == ProductId);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Product Obj)
        {
            if (ModelState.IsValid)
            {
                //if (Obj.Name == Obj.DisplayOrder.ToString())
                //{
                //    ModelState.AddModelError("Name", "Name cannot be exacly same with Display Order");
                //    return View();
                //}

                _productRepository.Update(Obj);
                _productRepository.Save();


                TempData["success"] = "Product has been updated!";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Product? obj = _productRepository.Get(c => c.Id  == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteAction(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == 0 || id == null)
                {
                    return NotFound();
                }

                Product obj = _productRepository.Get(c => c.Id==id);
                _productRepository.Delete(obj);
                _productRepository.Save();


                TempData["success"] = "Product has been deleted!";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
    }
}
