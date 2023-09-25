using AutoMapper;
using Task2.Data;
using Task2.DTOs;
using Task2.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task2.Repositories;


public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    private IAuthorRepository _authorRepositoryImplementation;

    public AuthorRepository(BookStoreDbContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsDtosAsync()
    {
        var authors = await _context.Authors
            .Include(author => author.Books)
            .ThenInclude(book => book.Genre)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    public Task<IEnumerable<Book>> GetAllBooksByAuthorAsync(Guid authorId)
    {
        return _authorRepositoryImplementation.GetAllBooksByAuthorAsync(authorId);
    }
}