using System;
using System.Threading.Tasks;
using AutoMapper;
using Task2.Data;
using Task2.Repositories;
using Task2.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UnitOfWork(BookStoreDbContext context, IMapper mapper) 
    {
        _context = context;
        _mapper = mapper;
        AuthorRepository = new AuthorRepository(_context, _mapper);
        BookRepository = new BookRepository(_context, _mapper);
        GenreRepository = new GenreRepository(_context, _mapper);
    }

    public IAuthorRepository AuthorRepository { get; }
    public IBookRepository BookRepository { get; }
    public IGenreRepository GenreRepository { get; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}