using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Task2.Data;
using Task2.DTOs;
using Task2.Models;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GenreRepository(BookStoreDbContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksByGenreAsync(string genreName)
    {
        var books = await _context.Books
            .Include(book => book.Author)
            .Include(book => book.Genre)
            .Where(book => book.Genre.Name.Equals(genreName, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();

        return _mapper.Map<IEnumerable<BookDto>>(books);
    }
}