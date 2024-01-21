using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages.Categories
{
    public class DeleteCategoryModel : PageModel
    {
        public readonly BookLibraryDbContext _dbContext;
        [BindProperty]
        public Category? category { get; set; }
        public string LoginUserEmail { get; set; } = string.Empty;
        public DeleteCategoryModel(BookLibraryDbContext bookLibraryDbContext)
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
        public async Task<IActionResult> OnPost(int id)
        {
            var cate = await _dbContext.Categories.FindAsync(id);

            if (cate != null)
            {
                _dbContext.Categories.Remove(cate);
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
            return _dbContext.Categories.Any(e => e.Id == id);
        }
    }
}
