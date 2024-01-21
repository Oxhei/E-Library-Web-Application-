using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Author { get; set; }
    public string ImageURL { get; set; }
    public string Title { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
}
