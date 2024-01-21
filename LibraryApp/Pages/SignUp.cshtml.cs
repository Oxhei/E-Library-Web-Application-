using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Pages
{
    public class SignUp : PageModel
    {
        private readonly BookLibraryDbContext _bookLibraryDbContext;
        public string ErrorMessage { get; set; } = string.Empty;
        public string LoginUserEmail { get; set; } = string.Empty;
        public SignUp(BookLibraryDbContext bookLibraryDbContext)
        {
            _bookLibraryDbContext = bookLibraryDbContext;
        }
        [BindProperty]
        public User user { get; set; }
        public void OnGet()
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(user.FirstName))
            {
                ErrorMessage = "User FirstName Missing..!";
                return Page();
            }
            if (string.IsNullOrEmpty(user.LastName))
            {
                ErrorMessage = "User LastName Missing..!";
                return Page();
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                ErrorMessage = "User Email Missing..!";
                return Page();
            }
            if (IsEmailExist(user.Email))
            {
                ErrorMessage = "User Email Already Exist..!";
                return Page();
            }
            _bookLibraryDbContext.Users.Add(user);
            await _bookLibraryDbContext.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        private bool IsEmailExist(string? email)
        {
            return _bookLibraryDbContext.Users.AsNoTracking().Any(e => e.Email == email);
        }
    }

}
