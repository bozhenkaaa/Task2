namespace Task2.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Author
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }

    [MaxLength(1000)]
    public string Biography { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}