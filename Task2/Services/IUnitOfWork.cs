using Task2.Repositories;

namespace Task2.Services;

public interface IUnitOfWork : IDisposable
{
    IAuthorRepository AuthorRepository { get; }
    IBookRepository BookRepository { get; }
    IGenreRepository GenreRepository { get; }
    Task<int> CompleteAsync();
}