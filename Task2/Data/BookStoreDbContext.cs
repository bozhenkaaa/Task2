using Microsoft.EntityFrameworkCore;
using Task2.Models;

namespace Task2.Data;

public class BookStoreDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(book =>
        {
            book.HasKey(b => b.Id);
            book.Property(b => b.Title).IsRequired().HasMaxLength(200);
            book.Property(b => b.AuthorId).IsRequired();
            book.Property(b => b.GenreId).IsRequired();
            book.Property(b => b.Price).IsRequired().HasColumnType("decimal(18, 2)");
            book.Property(b => b.QuantityAvailable).IsRequired();

            book.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            book.HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Author>(author =>
        {
            author.HasKey(a => a.Id);
            author.Property(a => a.FullName).IsRequired().HasMaxLength(100);
            author.Property(a => a.Biography).HasMaxLength(1000);
        });

        modelBuilder.Entity<Genre>(genre =>
        {
            genre.HasKey(g => g.Id);
            genre.Property(g => g.Name).IsRequired().HasMaxLength(100);
        });
    }
}