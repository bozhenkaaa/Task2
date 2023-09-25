## Bookstore API

This is a simple RESTful API for an online bookstore developed using ASP.NET Core and Entity Framework. Users can perform CRUD operations on books, authors, and genres, and can search for books by title, author, or genre.

## Getting Started

To run the API locally, you will need:

- [MsSql Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Rider](https://www.jetbrains.com/rider/) or your preferred IDE

### Installing

1. Clone this repository to your local machine.
2. Open the solution file (`.sln`) in Rider or your preferred IDE.
3. In the `appsettings.json` file, update the connection string values to match your local MsSql Server instance.
4. Run the database migrations to create the necessary tables in your MsSql Server instance. In the Package Manager Console, run the following command:
```
Update-Database
```
5. Build and run the project.

## Models

The following models are used in the API:

- `Book`: Represents a book in the bookstore. Properties include `Id`, `Title`, `Author`, `Genre`, `Price`, and `QuantityAvailable`.
- `Author`: Represents the author of a book. Properties include `Id`, `Name`, `Email`, `PhoneNumber`, and `Books`.
- `Genre`: Represents a genre that a book can belong to. Properties include `Id` and `Name`.

## Repositories

The following repository interfaces are defined:

- `IBookRepository`: Defines methods for performing CRUD operations on `Book` objects.
- `IAuthorRepository`: Defines methods for performing CRUD operations on `Author` objects.
- `IGenreRepository`: Defines methods for performing CRUD operations on `Genre` objects.

The following repository implementations are provided:

- `BookRepository`: Implements the `IBookRepository` interface using Entity Framework Core.
- `AuthorRepository`: Implements the `IAuthorRepository` interface using Entity Framework Core.
- `GenreRepository`: Implements the `IGenreRepository` interface using Entity Framework Core.

## Controllers

The following controllers are provided:

- `BooksController`: Provides endpoints for performing CRUD operations on `Book` objects.
- `AuthorsController`: Provides endpoints for performing CRUD operations on `Author` objects.
- `GenresController`: Provides endpoints for performing CRUD operations on `Genre` objects.

## Endpoints

The following endpoints are available for each controller:

### BooksController

- `GET /api/books`: Fetches all books
- `GET /api/books/{id}`: Fetches a single book by ID
- `POST /api/books`: Creates a new book
- `PUT /api/books/{id}`: Updates an existing book by ID
- `DELETE /api/books/{id}`: Deletes a book by ID
- `GET /api/books/search?title={title}`: Searches for books by title
- `GET /api/books/search?author={authorName}`: Searches for books by author name
- `GET /api/books/search?genre={genre}`: Searches for books by genre

### AuthorsController

- `GET /api/authors`: Fetches all authors
- `GET /api/authors/{id}`: Fetches a single author by ID
- `POST /api/authors`: Creates a new author
- `PUT /api/authors/{id}`: Updates an existing author by ID
- `DELETE /api/authors/{id}`: Deletes an author by ID

### GenresController

- `GET /api/genres`: Fetches all genres
- `GET /api/genres/{id}`: Fetches a single genre by ID
- `POST /api/genres`: Creates a new genre
- `PUT /api/genres/{id}`: Updates an existing genre by ID
- `DELETE /api/genres/{id}`: Deletes a genre by ID

## Built With

- ASP.NET Core
- Entity Framework Core
- MsSql Server

## Authors

- [bozhenka](https://github.com/bozhenkaaa) - Initial work

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Feedback

This task was not very difficult , but ChatGPT had a lot of problems, especially in writing unitTests, so UnitTests are not implemented in this program. It took me about 4 hours.

## Log

[chatbot_ui_prompts_history_9-25.log](https://github.com/bozhenkaaa/Task2/files/12713698/chatbot_ui_prompts_history_9-25.log)





