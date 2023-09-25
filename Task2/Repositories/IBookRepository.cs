using System.Collections.Generic;
using System.Threading.Tasks;
using Task2.Models;

namespace Task2.Repositories;

public interface IBookRepository : IRepository<Book>
{
    
    Task<IEnumerable<Book>> FindByTitleAsync(string title);
    Task<IEnumerable<Book>> FindByAuthorAsync(string fullName);
    Task<IEnumerable<Book>> FindByGenreAsync(string name);
}