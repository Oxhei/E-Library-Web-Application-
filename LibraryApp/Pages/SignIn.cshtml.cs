using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Pages
{
    public class SignIn : PageModel
    {
        private readonly BookLibraryDbContext _bookLibraryDbContext;
        public string ErrorMessage { get; set; } = string.Empty;
        public string LoginUserEmail { get; set; } = string.Empty;
        public SignIn(BookLibraryDbContext bookLibraryDbContext)
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
            if (string.IsNullOrEmpty(user.Email))
            {
                ErrorMessage = "User Email Missing..!";
                return Page();
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                ErrorMessage = "User Password Missing..!";
                return Page();
            }
            if (!IsEmailExist(user.Email))
            {
                ErrorMessage = "User Email Not Exist..!";
                return Page();
            }
            if (!IsUserExist(user.Email, user.Password))
            {
                ErrorMessage = "Please Try Again..!";
                return Page();
            }
            HttpContext.Session.SetString("LoginUserEmail", user.Email.ToString());
            return RedirectToPage("./Index");
        }
        private bool IsEmailExist(string? email)
        {
            return _bookLibraryDbContext.Users.AsNoTracking().Any(e => e.Email == email);
        }
        private bool IsUserExist(string? email, string? password)
        {
            var user = _bookLibraryDbContext.Users.AsNoTracking()
                .FirstOrDefault(e => e.Email.Equals(email) && e.Password.Equals(password));
            if (user != null && user.Email != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
