using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        public BookLibraryDbContext _dbContext;
        public string LoginUserEmail { get; set; } = string.Empty;
        public List<Category> categoryList { get; set; }
        public IndexModel(BookLibraryDbContext bookLibraryDbContext)
        {
            _dbContext=bookLibraryDbContext;
        }
        public void OnGet()
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
            categoryList =_dbContext.Categories.ToList();
        }
    }
}
