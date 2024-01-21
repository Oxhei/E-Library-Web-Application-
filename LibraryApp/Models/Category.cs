using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
