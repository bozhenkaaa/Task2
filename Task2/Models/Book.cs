using System.ComponentModel.DataAnnotations;

namespace Task2.Models;

public class Book
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    public Guid AuthorId { get; set; }

    public Author Author { get; set; }

    [Required]
    public Guid GenreId { get; set; }

    public Genre Genre { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int QuantityAvailable { get; set; }
}