using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWeb.Data;
using RazorWeb.Model;

namespace RazorWeb.Pages.Categories
{
    public class UpdateModel : PageModel
    {
        private readonly UdemyAssignmentRazorDBContext _dbContext;
        [BindProperty]
        public Category _category { get; set; }
        public UpdateModel(UdemyAssignmentRazorDBContext db)
        {
            _dbContext = db;
        }
        public void OnGet(int categoryId)
        {
            if(categoryId != null && categoryId != 0)
            {
                _category = _dbContext.categories.Find(categoryId);
            }
            else
            {
                TempData["failed"] = "Category Not Found";
                RedirectToPage("Index");
            }
        }

        public IActionResult OnPost()
        {
            _dbContext.categories.Update(_category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category has been updated";
            return RedirectToPage("Index");
        }
    }
}
