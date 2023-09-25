using Task2.Models;
using Task2.Repositories;
using Task2.Services;
using Moq;
using System.Collections.Generic;

namespace Task2.Tests
{
    [TestClass]
    public class BookstoreServicesTests
    {
        private Mock<IBookRepository> mockBookRepo;
        private Mock<IAuthorRepository> mockAuthorRepo;
        private Mock<IGenreRepository> mockGenreRepo;

        private IBookstoreServices bookstoreServices;

        [TestInitialize]
        public void Initialize()
        {
            mockBookRepo = new Mock<IBookRepository>();
            mockAuthorRepo = new Mock<IAuthorRepository>();
            mockGenreRepo = new Mock<IGenreRepository>();
            bookstoreServices = new BookstoreServices(mockBookRepo.Object, mockAuthorRepo.Object, mockGenreRepo.Object);
        }

        [TestMethod]
        public void GetAllBooks_ReturnsAllBooks()
        {
            // Arrange
            var expectedBooks = new List<Book>();
            mockBookRepo.Setup(repo => repo.GetAll()).Returns(expectedBooks);

            // Act
            var result = bookstoreServices.GetAllBooks();

            // Assert
            CollectionAssert.AreEqual(expectedBooks, result);
        }

        [TestMethod]
        public void GetBook_ReturnsBook()
        {
            // Arrange
            var bookId = 1;
            var expectedBook = new Book { BookId = bookId };
            mockBookRepo.Setup(repo => repo.Get(bookId)).Returns(expectedBook);

            // Act
            var result = bookstoreServices.GetBook(bookId);

            // Assert
            Assert.AreEqual(expectedBook, result);
        }

        // Add similar unit tests for GetBooksByTitle, GetBooksByAuthorName, GetBooksByGenre, AddBook, UpdateBook, and DeleteBook methods
    }
}