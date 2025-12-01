# Movie Catalog API

This repository contains a simple and extendable **Movie Catalog API** built with **C# (.NET 6)**.  
The solution demonstrates clean API design, layered architecture, and separation of concerns.

The API allows clients to manage and query movie data.  
It is designed to be lightweight, easy to extend, and ideal as a foundation for a larger library or media catalog system.

---

## Project Structure

- /src  
  - /MovieCatalogAPI  
    - Program.cs  
    - Controllers/  
      - MoviesController.cs  
    - Models/  
      - Movie.cs  
    - Services/  
      - IMovieService.cs  
      - MovieService.cs  

- /tests  
  - /MovieCatalogAPI.Tests  
    - MovieServiceTests.cs  

---

## Key Concepts

### Movie Management
The API supports:

- Listing all movies  
- Fetching a movie by ID  
- Searching by title (partial case-insensitive match)  
- Adding, updating, and deleting movies (if implemented)  

The API currently uses in-memory storage but can easily be extended to a database (SQL, MongoDB, EF Core, etc.).

### Layered Architecture
The solution follows a clean architecture layout:

- **Controllers** → handle HTTP requests  
- **Services** → contain business logic  
- **Models** → define the domain structures  

This pattern keeps the system testable and easy to maintain.

### Testability
The service layer is fully isolated and can be unit tested without web hosting.  
Tests validate core logic such as:

- Adding movies  
- Searching  
- Validating duplicates  
- Retrieving items safely  

---

## Usage Example
```bash
var service = new MovieService();

service.AddMovie(new Movie
{
    Id = 1,
    Title = "Inception",
    Genre = "Sci-Fi",
    Year = 2010
});

var result = service.Search("inception");
// result → list with the matching movie
```

---

## Design Principles
- SOLID
- Clean controller–service architecture
- Extensibility
- Testability
- Minimal dependencies

---

## Requirements
- .NET 6 SDK
- xUnit (or your chosen test framework)

---

## Running the API
Build:
```bash
dotnet build
```

Run:
```bash
dotnet run
```

Default URLs (depending on your config):
```bash
https://localhost:5001
http://localhost:5000
```
Swagger/OpenAPI is typically available at:
```bash
https://localhost:5001/swagger
```