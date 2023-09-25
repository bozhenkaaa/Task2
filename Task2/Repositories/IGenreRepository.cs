using Task2.DTOs;
using Task2.Models;
using Task2.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    Task<IEnumerable<BookDto>> GetAllBooksByGenreAsync(string genreName);
}