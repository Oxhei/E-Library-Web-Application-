using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages.Categories
{
    public class AddCategoryModel : PageModel
    {
        public BookLibraryDbContext _dbContext;
        public string ErrorMessage { get; set; } = string.Empty;
        public string LoginUserEmail { get; set; } = string.Empty;
        [BindProperty]
        public Category category { get; set; }
        public AddCategoryModel(BookLibraryDbContext bookLibraryDbContext)
        {
            _dbContext = bookLibraryDbContext;

        }
        public void OnGet()
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
        }
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                ErrorMessage = "Category Name Missing..!";
                return Page();
            }
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
