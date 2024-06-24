using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using Model.Models;
using DataAccess.Repositories;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryController(UdemyAssignmentDBContext db)
        {
            _categoryRepository = new CategoryRepository(db);
        }
        public IActionResult Index()
        {
            List<Category> ObjCategories = _categoryRepository.GetAll().ToList();
            return View(ObjCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category Obj)
        {
            if (ModelState.IsValid)
            {
                if (Obj.Name == Obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "Name cannot be exacly same with Display Order");
                    return View();
                }
                _categoryRepository.Add(Obj);
                _categoryRepository.Save();

                TempData["success"] = "Category has been created!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Update(int? categoryId)
        {
            if (categoryId == 0 || categoryId == null)
            {
                return NotFound();
            }

            Category? obj = _categoryRepository.Get(c => c.Id == categoryId);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Category Obj)
        {
            if (ModelState.IsValid)
            {
                if (Obj.Name == Obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "Name cannot be exacly same with Display Order");
                    return View();
                }

                _categoryRepository.Update(Obj);
                _categoryRepository.Save();


                TempData["success"] = "Category has been updated!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? obj = _categoryRepository.Get(c => c.Id  == id);

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

                Category obj = _categoryRepository.Get(c => c.Id==id);
                _categoryRepository.Delete(obj);
                _categoryRepository.Save();


                TempData["success"] = "Category has been deleted!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}
