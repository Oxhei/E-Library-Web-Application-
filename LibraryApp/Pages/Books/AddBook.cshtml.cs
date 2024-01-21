using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages.Books
{
    public class AddBookModel : PageModel
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public readonly BookLibraryDbContext _dbContext;
        public string ErrorMessage { get; set; } = string.Empty;
        public string LoginUserEmail { get; set; } = string.Empty;
        [BindProperty]
        public Book book { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public List<Category> CategoriesList { get; set; }
        public AddBookModel(BookLibraryDbContext bookLibraryDbContext, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _dbContext = bookLibraryDbContext;
            _environment = environment;
        }
        public void OnGet()
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
            CategoriesList = _dbContext.Categories.ToList();
        }
        public async Task<IActionResult> OnPost()
        {
            if (book.CategoryId == 0)
            {
                ErrorMessage = "Book Category Missing..!";
                CategoriesList = _dbContext.Categories.ToList();
                return Page();
            }
            if (string.IsNullOrEmpty(book.Title))
            {
                ErrorMessage = "Book Title Missing..!";
                CategoriesList = _dbContext.Categories.ToList();
                return Page();
            }
            if (string.IsNullOrEmpty(book.Author))
            {
                ErrorMessage = "Book Author Missing..!";
                CategoriesList = _dbContext.Categories.ToList();
                return Page();
            }
            if (string.IsNullOrEmpty(Upload.FileName))
            {
                ErrorMessage = "Book Image Missing..!";
                CategoriesList = _dbContext.Categories.ToList();
                return Page();
            }
            var imgPath = Guid.NewGuid().ToString() + "_" + Upload.FileName;
            var bookImageRootPath = "\\Images\\BookImages";
            //var file = Path.Combine(_environment.ContentRootPath, "Images/BookImages", Upload.FileName);
            var file = Path.Combine(_environment.WebRootPath + "/Images/BookImages/", imgPath);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
            book.ImageURL = bookImageRootPath + "\\" + imgPath;
            book.CreateDate = DateTime.Now;
            //save Image on server
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
