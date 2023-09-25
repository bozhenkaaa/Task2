using Task2.Models;

namespace Task2.Repositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<IEnumerable<Book>> GetAllBooksByAuthorAsync(Guid authorId);
}