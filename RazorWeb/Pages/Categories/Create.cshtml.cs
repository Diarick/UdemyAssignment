using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWeb.Data;
using RazorWeb.Model;

namespace RazorWeb.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly UdemyAssignmentRazorDBContext _dbContext;
        [BindProperty]
        public Category _category { get; set; }
        public CreateModel(UdemyAssignmentRazorDBContext db)
        {
            _dbContext = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _dbContext.categories.Add(_category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category has been created";
            return RedirectToPage("Index");
        }
    }
}
