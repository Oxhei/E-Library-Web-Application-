using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Pages.Users
{
    public class DeleteUserModel : PageModel
    {
        public readonly BookLibraryDbContext _dBContext;
        public string LoginUserEmail { get; set; } = string.Empty;
        [BindProperty]
        public User? user { get; set; }
        public DeleteUserModel(BookLibraryDbContext bookLibraryDbContext)
        {
            _dBContext = bookLibraryDbContext;
        }
        public IActionResult OnGet(int? id)
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
            if (!UserExists(id))
            {
                return RedirectToPage("./Index");
            }
            user = _dBContext.Users.FirstOrDefault(x => x.Id == id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!UserExists(user.Id))
            {
                return RedirectToPage("./Index");
            }
            if (user != null)
            {
                _dBContext.Users.Remove(user);
                await _dBContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        private bool UserExists(int? id)
        {
            if (id == null || id == 0)
            {
                return false;
            }
            return _dBContext.Users.AsNoTracking().Any(e => e.Id == id);
        }
    }
}
