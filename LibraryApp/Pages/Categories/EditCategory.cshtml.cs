using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Pages.Categories
{
    public class EditCategoryModel : PageModel
    {
        public readonly BookLibraryDbContext _dbContext;
        public string LoginUserEmail { get; set; } = string.Empty;
        [BindProperty]
        public Category? category { get; set; }
        public EditCategoryModel(BookLibraryDbContext bookLibraryDbContext)
        {
            _dbContext = bookLibraryDbContext;
        }
        public IActionResult OnGet(int? id)
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
            if (!CategoryExists(id))
            {
                return RedirectToPage("./Index");
            }
            category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!CategoryExists(category.Id))
            {
                return RedirectToPage("./Index");
            }
            if (category != null)
            {
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        private bool CategoryExists(int? id)
        {
            if (id == null || id == 0)
            {
                return false;
            }
            return _dbContext.Categories.AsNoTracking().Any(e => e.Id == id);
        }
    }
}
