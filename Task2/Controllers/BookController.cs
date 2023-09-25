using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.DTOs;
using Task2.Models;
using Task2.Services;

[ApiController]
[Route("api/controller")]
public class BooksController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BooksController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var books = await _unitOfWork.BookRepository.GetAllAsync();
        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
        return Ok(booksDto);
    }
    

    [HttpGet("{id}", Name = "GetBookByIdAsync")]
    public async Task<IActionResult> GetBookByIdAsync(Guid id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        var bookDto = _mapper.Map<BookDto>(book);
        return Ok(bookDto);
    }
    [HttpPost]
    public async Task<IActionResult> CreateBookAsync([FromBody] BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        await _unitOfWork.BookRepository.AddAsync(book);
        await _unitOfWork.CompleteAsync();

        bookDto.Id = book.Id;
        return CreatedAtRoute("GetBookByIdAsync", new { id = bookDto.Id }, bookDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookAsync(Guid id, [FromBody] BookDto bookDto)
    {
        var bookToUpdate = await _unitOfWork.BookRepository.GetByIdAsync(id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        _mapper.Map(bookDto, bookToUpdate);
        _unitOfWork.BookRepository.Update(bookToUpdate);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync(Guid id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        _unitOfWork.BookRepository.Remove(book);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpGet("search/{title}")]
    public async Task<IActionResult> SearchBooksByTitleAsync([FromRoute] string title)
    {
        var books = await _unitOfWork.BookRepository.FindByTitleAsync(title);
        if (books == null)
        {
            return NotFound();
        }
        
        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
        return Ok(booksDto);
    }
    [HttpGet("genre/{genreName}")]
    public async Task<IActionResult> GetBooksByGenreAsync(string genreName)
    {
        var books = await _unitOfWork.GenreRepository.GetAllBooksByGenreAsync(genreName);

        if (books == null || !books.Any())
        {
            return NotFound();
        }

        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
        return Ok(booksDto);
    }

    [HttpGet("author/{authorId}")]
    public async Task<IActionResult> GetBooksByAuthorAsync(Guid authorId)
    {
        var books = await _unitOfWork.AuthorRepository.GetAllBooksByAuthorAsync(authorId);

        if (books == null || !books.Any())
        {
            return NotFound();
        }

        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
        return Ok(booksDto);
    }
}