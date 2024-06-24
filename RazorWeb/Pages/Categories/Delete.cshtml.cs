using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWeb.Data;
using RazorWeb.Model;

namespace RazorWeb.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly UdemyAssignmentRazorDBContext _dbContext;
        [BindProperty]
        public Category _category { get; set; }

        public DeleteModel(UdemyAssignmentRazorDBContext db)
        {
            _dbContext = db;
        }
        public void OnGet(int? Id)
        {
            if (Id != null && Id != 0)
            {
                _category = _dbContext.categories.Find(Id);
            }
            else
            {
                TempData["failed"] = "Category Not Found";
                RedirectToPage("Index");
            }
        }
        public IActionResult OnPost() { 
            if (_category.Id == null && _category.Id == 0)
            {
                TempData["failed"] = "Category Not Found";
                RedirectToPage("Index");
            }
            _dbContext.categories.Remove(_category);
            _dbContext.SaveChanges();

            TempData["success"] = "Category has been deleted";
            return RedirectToPage("Index");
        }
    }
}
