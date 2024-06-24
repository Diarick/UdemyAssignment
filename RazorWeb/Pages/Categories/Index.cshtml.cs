using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWeb.Data;
using RazorWeb.Migrations;
using RazorWeb.Model;

namespace RazorWeb.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly UdemyAssignmentRazorDBContext _dbContext;
        public List<Category> _categoryList { get; set; }

        public IndexModel(UdemyAssignmentRazorDBContext db)
        {
            _dbContext = db;
        }
        public void OnGet()
        {
            _categoryList = _dbContext.categories.ToList();
        }
    }
}
