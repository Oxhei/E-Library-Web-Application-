using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Pages.Books
{
    public class DeleteBookModel : PageModel
    {
        public readonly BookLibraryDbContext _dbContext;
        public DeleteBookModel(BookLibraryDbContext bookLibraryDbContext)
        {
            _dbContext = bookLibraryDbContext;
        }
        public List<Category> CategoriesList { get; set; }
        public int SelectedCate { get; set; } = 0;
        public string SelectedImage { get; set; } = string.Empty;
        public string LoginUserEmail { get; set; } = string.Empty;
        [BindProperty]
        public Book? book { get; set; }
        public IActionResult OnGet(int? id)
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
            if (!BookExists(id))
            {
                return RedirectToPage("./Index");
            }
            CategoriesList = _dbContext.Categories.ToList();
            book = _dbContext.Books.FirstOrDefault(x => x.Id == id);
            SelectedCate = book.CategoryId;
            SelectedImage = book.ImageURL;
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);

            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        private bool BookExists(int? id)
        {
            if (id == null || id == 0)
            {
                return false;
            }
            return _dbContext.Books.Any(e => e.Id == id);
        }
    }
}
