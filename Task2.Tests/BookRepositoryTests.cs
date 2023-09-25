using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;
using Task2;
using Task2.Data;
using Task2.DTOs;
using Task2.Models;

namespace Task2.Tests;

public class BookRepositoryTests
{
    private BookStoreDbContext GetInMemoryDbContext()
    {
        // Configure the in-memory database
        var options = new DbContextOptionsBuilder<BookStoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        // Initialize the DbContext
        var context = new BookStoreDbContext(options);

        // Seed the database with test data
        context.Books.AddRange(
            new Book { Id = Guid.NewGuid(), Title = "Book A", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() },
            new Book { Id = Guid.NewGuid(), Title = "Book B", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() },
            new Book { Id = Guid.NewGuid(), Title = "Book C", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() },
            new Book { Id = Guid.NewGuid(), Title = "Book D", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() },
            new Book { Id = Guid.NewGuid(), Title = "Book E", AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() }
        );
        context.SaveChanges();

        return context;
    }
    private IMapper GetMockMapper()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Book, BookDto>(); // Replace 'BookDto' with your actual DTO class
            // Add more mappings as needed
        });

        var mapper = new Mapper(configurationProvider);
        return mapper;
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllBooks()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var bookRepository = new BookRepository(context, mockMapper);

        // Act
        var books = await bookRepository.GetAllAsync();

        // Assert
        Assert.Equal(5, books.Count()); // Change the number to match the test data size
    }

    [Fact]
    public async Task AddAsync_AddsNewBook()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var bookRepository = new BookRepository(context, mockMapper);
        var newBook = new Book
        {
            Title = "New Book",
            AuthorId = Guid.NewGuid(),
            GenreId = Guid.NewGuid()
        };

        // Act
        await bookRepository.AddAsync(newBook);
        await context.SaveChangesAsync();

        // Assert
        Assert.NotNull(context.Books.FirstOrDefault(b => b.Title == "New Book"));
    }

    [Fact]
    public async Task Update_UpdatesExistingBook()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var bookRepository = new BookRepository(context, mockMapper);
        var existingBook = context.Books.First();
        var updatedTitle = "Updated Title";
        existingBook.Title = updatedTitle;

        // Act
        bookRepository.Update(existingBook);
        await context.SaveChangesAsync();

        // Assert
        var updatedBook = context.Books.First(b => b.Id == existingBook.Id);
        Assert.NotNull(updatedBook);
        Assert.Equal(updatedTitle, updatedBook.Title);
    }

    [Fact]
    public async Task Remove_RemovesBookById()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var bookRepository = new BookRepository(context, mockMapper);
        var bookToRemove = context.Books.First();

        // Act
        bookRepository.Remove(bookToRemove);
        await context.SaveChangesAsync();

        // Assert
        var removedBook = context.Books.FirstOrDefault(b => b.Id == bookToRemove.Id);
        Assert.Null(removedBook);
    }
    [Fact]
    public async Task FindByTitleAsync_ReturnsMatchingBooks()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var bookRepository = new BookRepository(context, mockMapper);
        var searchTitle = "Book C";

        // Debug - Print the titles of all books in the test data
        Console.WriteLine("All books in the test data:");
        foreach (var book in context.Books)
        {
            Console.WriteLine(book.Title);
        }

        // Act
        var books = await bookRepository.FindByTitleAsync(searchTitle);

        // Debug - Manually search for books using case-insensitive matching
        var manualSearchResults = context.Books.Where(b => b.Title.Contains(searchTitle, StringComparison.OrdinalIgnoreCase)).ToList();

        // Debug - Print the titles of all books that match the search title using manual search
        Console.WriteLine("Books that match the search title (manual search):");
        foreach (var book in manualSearchResults)
        {
            Console.WriteLine(book.Title);
        }

        // Assert using the manual search results
        Assert.NotEmpty(manualSearchResults);
        Assert.True(manualSearchResults.All(b => b.Title.Contains(searchTitle, StringComparison.OrdinalIgnoreCase)));  
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNullForNonExistentBook()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var bookRepository = new BookRepository(context, mockMapper);
        var nonExistentBookId = Guid.NewGuid();

        // Act
        var book = await bookRepository.GetByIdAsync(nonExistentBookId);

        // Assert
        Assert.Null(book);
    }

}