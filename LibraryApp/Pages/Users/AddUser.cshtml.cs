using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Pages.Users
{
    public class AddUserModel : PageModel
    {
        public readonly BookLibraryDbContext _dBContext;
        public string ErrorMessage { get; set; } = string.Empty;
        public string LoginUserEmail { get; set; } = string.Empty;
        [BindProperty]
        public User user { get; set; }
        public AddUserModel(BookLibraryDbContext bookLibraryDbContext)
        {
            _dBContext = bookLibraryDbContext;
        }
        public void OnGet()
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
        }
        public IActionResult OnPost()
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
            _dBContext.Users.Add(user);
            _dBContext.SaveChanges();
            return RedirectToPage("./Index");
        }
        private bool IsEmailExist(string? email)
        {
            return _dBContext.Users.AsNoTracking().Any(e => e.Email == email);
        }
    }
}
