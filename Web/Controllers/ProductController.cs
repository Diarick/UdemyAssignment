using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using Model.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Authorization;
using Utility;

namespace Web.Controllers
{
    [Authorize(Roles = BaseUtility.Role_User_Admin)]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(UdemyAssignmentDBContext db, IWebHostEnvironment web)
        {
            _productRepository = new ProductRepository(db);
            _categoryRepository = new CategoryRepository(db);
            _webHostEnvironment = web;
        }
        public IActionResult Index()
        {
            List<Product> ObjProducts = _productRepository.GetAll("Category").ToList();
            
            return View(ObjProducts);
        }
        public IActionResult Upsert(int? ProductId) // Update & Insert
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

            if(ProductId != null && ProductId != 0) 
            { 
                product.product = _productRepository.Get(u => u.Id == ProductId);
            }

            return View(product);
        }
        [HttpPost]
        public IActionResult Upsert(ProductViewModel Obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(Obj.product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, Obj.product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var filestream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }

                    Obj.product.ImageUrl = @"\images\product\" + filename;
                }

                if(Obj.product.Id == 0 || Obj.product.Id == null)
                { 
                    _productRepository.Add(Obj.product);
                    TempData["success"] = "Product has been created!";
                }
                else 
                { 
                    _productRepository.Update(Obj.product);
                    TempData["success"] = "Product has been updated!";
                }
                _productRepository.Save();
                return RedirectToAction("Index", "Product");
            }

            Obj.categories = _categoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(Obj);
        }

        public IActionResult Detail(int? id)
        {
            Product product = _productRepository.GetAll("Category").Where(u => u.Id == id).FirstOrDefault();

            return View(product);
        }
        //public IActionResult Update(int? ProductId)
        //{
        //    if (ProductId == 0 || ProductId == null)
        //    {
        //        return NotFound();
        //    }

        //    Product? obj = _productRepository.Get(c => c.Id == ProductId);

        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(obj);
        //}
        //[HttpPost]
        //public IActionResult Update(Product Obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //if (Obj.Name == Obj.DisplayOrder.ToString())
        //        //{
        //        //    ModelState.AddModelError("Name", "Name cannot be exacly same with Display Order");
        //        //    return View();
        //        //}

        //        _productRepository.Update(Obj);
        //        _productRepository.Save();


        //        TempData["success"] = "Product has been updated!";
        //        return RedirectToAction("Index", "Product");
        //    }
        //    return View();
        //}
        //public IActionResult Delete(int? id)
        //{
        //    if (id == 0 || id == null)
        //    {
        //        return NotFound();
        //    }

        //    Product? obj = _productRepository.Get(c => c.Id  == id);

        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(obj);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteAction(int? id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id == 0 || id == null)
        //        {
        //            return NotFound();
        //        }

        //        Product obj = _productRepository.Get(c => c.Id==id);
        //        _productRepository.Delete(obj);
        //        _productRepository.Save();


        //        TempData["success"] = "Product has been deleted!";
        //        return RedirectToAction("Index", "Product");
        //    }
        //    return View();
        //}

        #region api call
        public IActionResult GetAll()
        {
            List<Product> ObjProducts = _productRepository.GetAll("Category").ToList();
            return Json(new {data = ObjProducts});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (ModelState.IsValid)
            {
                Product DeletedProduct = _productRepository.Get(u => u.Id == id);

                if(DeletedProduct == null) {
                    return Json(new { status = false, message = "Product not found" });
                }

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (DeletedProduct.ImageUrl != null)
                {
                    if (System.IO.File.Exists(DeletedProduct.ImageUrl))
                    {
                        System.IO.File.Delete(DeletedProduct.ImageUrl);
                    }
                }

                _productRepository.Delete(DeletedProduct);
                _productRepository.Save();
                return Json(new { status = true, message = "Product has been deleted" });
            }
            return Json(new { status = false, message = "Product is invalid" });
        }
        #endregion
    }
}
