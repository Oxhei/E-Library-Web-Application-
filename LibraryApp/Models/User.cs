using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    [NotMapped]
    public string ConfirmPassword { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
}
