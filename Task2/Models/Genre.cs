using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task2.Models;

public class Genre
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}