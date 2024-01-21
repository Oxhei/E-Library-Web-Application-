using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages
{
    public class About : PageModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string LoginUserEmail { get; set; } = string.Empty;
        // Add other properties as needed
        public void OnGet()
        {
            LoginUserEmail = HttpContext.Session.GetString("LoginUserEmail") as string ?? string.Empty;
        }
    }

}
