using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages.Users
{
    public class IndexModel : PageModel
    {
        public readonly BookLibraryDbContext _dBContext;
        public string LoginUserEmail { get; set; } = string.Empty;
        [BindProperty]
        public List<User> UserList { get; set; }
        public IndexModel(BookLibraryDbContext bookLibraryDbContext)
        {
            _dBContext = bookLibraryDbContext;
        }
        public void OnGet()
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
            UserList = _dBContext.Users.ToList();
        }
    }
}
