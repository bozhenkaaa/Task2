using System.ComponentModel.DataAnnotations;

namespace Task2.DTOs;


public class AuthorDto
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }

    [MaxLength(1000)]
    public string Biography { get; set; }
}