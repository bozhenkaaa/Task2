namespace Task2.DTOs;

using System.ComponentModel.DataAnnotations;

public class BookDto
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    public Guid AuthorId { get; set; }

    [Required]
    public Guid GenreId { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int QuantityAvailable { get; set; }
}