namespace LibraryApp.ModelDto;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class BookDto
{
    public int BookCategoryId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public string BookAuthor { get; set; }
    public string BookImagePath { get; set; }
}
