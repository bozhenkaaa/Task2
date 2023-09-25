using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Task2;
using Task2.Data;
using Task2.DTOs;
using Task2.Models;

public class AuthorRepositoryTests
{
    private BookStoreDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<BookStoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new BookStoreDbContext(options);

        // Seed the database with test data for authors
        context.Authors.AddRange(
            new Author { Id = Guid.NewGuid(), FullName = "Author A", Biography = "Biography A" },
            new Author { Id = Guid.NewGuid(), FullName = "Author B", Biography = "Biography B" },
            new Author { Id = Guid.NewGuid(), FullName = "Author C", Biography = "Biography C" },
            new Author { Id = Guid.NewGuid(), FullName = "Author D", Biography = "Biography D" },
            new Author { Id = Guid.NewGuid(), FullName = "Author E", Biography = "Biography E" }
        );
        context.SaveChanges();

        return context;
    }

    private IMapper GetMockMapper()
    {
        // Replace with your actual AutoMapper configuration and DTO classes.
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Author, AuthorDto>();
        });

        var mapper = new Mapper(configurationProvider);
        return mapper;
    }
    
    // Test GetAllAsync method
    [Fact]
    public async Task GetAllAsync_ReturnsAllAuthors()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var authorRepository = new AuthorRepository(context, mockMapper);

        // Act
        var authors = await authorRepository.GetAllAsync();

        // Assert
        Assert.Equal(5, authors.Count());
    }

// Test GetByIdAsync method
    [Fact]
    public async Task GetByIdAsync_ReturnsAuthorById()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var authorRepository = new AuthorRepository(context, mockMapper);
        var existingAuthor = context.Authors.First();

        // Act
        var author = await authorRepository.GetByIdAsync(existingAuthor.Id);

        // Assert
        Assert.NotNull(author);
        Assert.Equal(existingAuthor.Id, author.Id);
        Assert.Equal(existingAuthor.FullName, author.FullName);
    }

// Test GetByIdAsync method for non-existent author
    [Fact]
    public async Task GetByIdAsync_ReturnsNullForNonExistentAuthor()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var authorRepository = new AuthorRepository(context, mockMapper);
        var nonExistentAuthorId = Guid.NewGuid();

        // Act
        var author = await authorRepository.GetByIdAsync(nonExistentAuthorId);

        // Assert
        Assert.Null(author);
    }
}