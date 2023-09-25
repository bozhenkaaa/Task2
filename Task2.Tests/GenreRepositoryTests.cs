using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Task2;
using Task2.Data;
using Task2.DTOs;
using Task2.Models;

public class GenreRepositoryTests
{
    private BookStoreDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<BookStoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new BookStoreDbContext(options);

        // Seed the database with test data for genres
        context.Genres.AddRange(
            new Genre { Id = Guid.NewGuid(), Name = "Genre A" },
            new Genre { Id = Guid.NewGuid(), Name = "Genre B" },
            new Genre { Id = Guid.NewGuid(), Name = "Genre C" },
            new Genre { Id = Guid.NewGuid(), Name = "Genre D" },
            new Genre { Id = Guid.NewGuid(), Name = "Genre E" }
        );
        context.SaveChanges();

        return context;
    }

    private IMapper GetMockMapper()
    {
        // Replace with your actual AutoMapper configuration and DTO classes.
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Genre, GenreDto>();
        });

        var mapper = new Mapper(configurationProvider);
        return mapper;
    }
    
    // Test GetAllAsync method
    [Fact]
    public async Task GetAllAsync_ReturnsAllGenres()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var genreRepository = new GenreRepository(context, mockMapper);

        // Act
        var genres = await genreRepository.GetAllAsync();

        // Assert
        Assert.Equal(5, genres.Count());
    }

// Test GetByIdAsync method
    [Fact]
    public async Task GetByIdAsync_ReturnsGenreById()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var genreRepository = new GenreRepository(context, mockMapper);
        var existingGenre = context.Genres.First();

        // Act
        var genre = await genreRepository.GetByIdAsync(existingGenre.Id);

        // Assert
        Assert.NotNull(genre);
        Assert.Equal(existingGenre.Id, genre.Id);
        Assert.Equal(existingGenre.Name, genre.Name);
    }

// Test GetByIdAsync method for non-existent genre
    [Fact]
    public async Task GetByIdAsync_ReturnsNullForNonExistentGenre()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockMapper = GetMockMapper();
        var genreRepository = new GenreRepository(context, mockMapper);
        var nonExistentGenreId = Guid.NewGuid();

        // Act
        var genre = await genreRepository.GetByIdAsync(nonExistentGenreId);

        // Assert
        Assert.Null(genre);
    }
}