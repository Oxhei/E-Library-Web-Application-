using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Pages.Books
{
    public class EditBookModel : PageModel
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public readonly BookLibraryDbContext _dbContext;
        public List<Category> CategoriesList { get; set; }
        public int SelectedCate { get; set; } = 0;
        public string SelectedImage { get; set; } = string.Empty;
        public string LoginUserEmail { get; set; } = string.Empty;
        [BindProperty]
        public Book? book { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public EditBookModel(BookLibraryDbContext bookLibraryDbContext, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _dbContext = bookLibraryDbContext;
            _environment = environment;
        }
        public IActionResult OnGet(int? id)
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
            if (!BookExists(id))
            {
                return RedirectToPage("./Index");
            }
            CategoriesList = _dbContext.Categories.ToList();
            book = _dbContext.Books.AsNoTracking().FirstOrDefault(x => x.Id == id);
            SelectedCate = book.CategoryId;
            SelectedImage = book.ImageURL;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!BookExists(book.Id))
            {
                return RedirectToPage("./Index");
            }
            if (book != null)
            {
                if (Upload == null)
                {
                    book.ImageURL = GetOldImageURL(book.Id).ImageURL;
                    book.CreateDate = GetOldImageURL(book.Id).CreateDate;
                }
                else
                {
                    var imgPath = Guid.NewGuid().ToString() + "_" + Upload.FileName;
                    var bookImageRootPath = "\\Images\\BookImages";
                    //var file = Path.Combine(_environment.ContentRootPath, "Images/BookImages", Upload.FileName);
                    var file = Path.Combine(_environment.WebRootPath + "/Images/BookImages/", imgPath);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Upload.CopyToAsync(fileStream);
                    }
                    book.ImageURL = bookImageRootPath + "\\" + imgPath;
                }
                _dbContext.Books.Update(book);
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
            return _dbContext.Books.AsNoTracking().Any(e => e.Id == id);
        }
        private Book GetOldImageURL(int? id)
        {
            if (id == null || id == 0)
            {
                return new Book();
            }
            return _dbContext.Books.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }
    }
}
