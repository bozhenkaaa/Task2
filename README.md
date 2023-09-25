# Bookstore API

This is a Bookstore API application that manages `Book`, `Author`, and `Genre` entities. The application is built using ASP.NET Core, Entity Framework Core, and AutoMapper.

## Application Structure

The main components of the application include models, repositories, and controllers.

**Models:**

- `Book`: Represents a book entity with properties such as Title, Author, and Genre.
- `Author`: Represents an author entity with properties such as FullName and Biography.
- `Genre`: Represents a genre entity with a Name property.

**Repositories:**

- `BookRepository`: Provides methods for interacting with the database for book entities, like `GetAllAsync`, `GetByIdAsync`, and `FindByTitleAsync`.
- `AuthorRepository`: Provides methods for interacting with the database for author entities, like `GetAllAsync` and `GetByIdAsync`.
- `GenreRepository`: Provides methods for interacting with the database for genre entities, like `GetAllAsync` and `GetByIdAsync`.

Each repository is backed by an interface to facilitate Dependency Injection.

**Controllers:**

- `BooksController`: API endpoints for managing Book entities.
- `AuthorsController`: API endpoints for managing Author entities.
- `GenresController`: API endpoints for managing Genre entities.

These controllers provide CRUD operations for each entity via RESTful API endpoints.

## Getting Started

1. Clone this repository.
2. Open the Bookstore API solution in Visual Studio or your preferred IDE.
3. Ensure that the necessary NuGet packages are installed, such as Entity Framework Core and AutoMapper.
4. Update the `appsettings.json` file with the appropriate database connection string for your development environment.
5. Open the terminal or command prompt, navigate to the project directory, and execute the following command to apply migrations to create the database:

```bash
dotnet ef database update
```
6. Set the API project as the startup project in the solution.
7. Run the application using the "Run" button in Visual Studio or by executing `dotnet run` in the terminal or command prompt.
8. Use a tool like [Postman](https://www.postman.com/) or [Swagger](https://swagger.io/) to interact with the RESTful API endpoints for books, authors, and genres.

For more information on developing applications with ASP.NET Core, Entity Framework Core, and AutoMapper, please refer to the official documentation:

- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org/)

## Authors

- [bozhenka](https://github.com/bozhenkaaa) - Initial work

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Feedback

This task was not very difficult , but ChatGPT had a lot of problems, especially in writing unitTests. It took me about 4 hours.

## Log

[chatbot_ui_prompts_history_9-25.log](https://github.com/bozhenkaaa/Task2/files/12719260/chatbot_ui_prompts_history_9-25.log)






