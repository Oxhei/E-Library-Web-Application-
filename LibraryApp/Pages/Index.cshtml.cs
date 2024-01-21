using LibraryApp.ModelDto;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BookLibraryDbContext _dbContext;
        [BindProperty]
        public List<CategoryDto> categories { get; set; }
        [BindProperty]
        public List<BookDto> BookList { get; set; }
        public string LoginUserEmail { get; set; } = string.Empty;

        public IndexModel(ILogger<IndexModel> logger, BookLibraryDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
            categories = (from cate in _dbContext.Categories
                          select new CategoryDto
                          {
                              Id = cate.Id,
                              Name = cate.Name
                          }).ToList();
            BookList = (from cate in _dbContext.Categories
                        join book in _dbContext.Books on cate.Id equals book.CategoryId
                        select new BookDto
                        {
                            BookCategoryId = cate.Id,
                            BookTitle = book.Title,
                            BookAuthor = book.Author,
                            BookId = book.Id,
                            BookImagePath = book.ImageURL
                        }).ToList();
        }
    }
}
