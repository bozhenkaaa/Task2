using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task2.Data;
using Task2.Models;
using Task2.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookRepository(BookStoreDbContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Book>> FindByTitleAsync(string title)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Where(b => b.Title.ToLower().Contains(title.ToLower()))
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> FindByAuthorAsync(string fullName)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Where(b => b.Author.FullName.Contains(fullName, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> FindByGenreAsync(string name)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Where(b => b.Genre.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
    }

    // Implement custom methods for the Book entity here, if needed.
}