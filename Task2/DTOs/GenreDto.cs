namespace Task2.DTOs;

using System.ComponentModel.DataAnnotations;

public class GenreDto
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}