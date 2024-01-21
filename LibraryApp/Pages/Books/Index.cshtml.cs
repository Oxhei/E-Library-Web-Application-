using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages.Books
{
    public class IndexModel : PageModel
    {
        public BookLibraryDbContext _dbContext;
        public List<Book> bookList { get; set; }
        public string LoginUserEmail { get; set; } = string.Empty;
        public IndexModel(BookLibraryDbContext bookLibraryDbContext)
        {
            _dbContext = bookLibraryDbContext;
        }
        public void OnGet()
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
            bookList = _dbContext.Books.ToList();
        }
    }
}
