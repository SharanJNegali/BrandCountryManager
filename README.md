# Brand Country Manager

A comprehensive ASP.NET Core Web API project implementing CQRS pattern for managing brands and countries with proper separation of concerns.

## Overview

This project demonstrates modern .NET 8 development practices including:
- **CQRS (Command Query Responsibility Segregation)** pattern
- **Controller-Mapper-Handler** architecture
- **Repository Pattern** with Entity Framework Core
- **MediatR** for handling commands and queries
- **FluentValidation** for input validation
- **Global Exception Handling** middleware
- **Swagger/OpenAPI** documentation
- **Entity Framework Code First** with seed data

## Architecture

### Project Structure

```
BrandCountryManager/
├── Models/
│   ├── Entities/           # Domain entities (Country, Brand)
│   └── DTOs/              # Data Transfer Objects
├── Data/
│   └── ApplicationDbContext.cs  # EF Core context
├── Repositories/          # Repository interfaces and implementations
├── Mappers/              # Static mapping between entities and DTOs
├── Features/             # CQRS Commands and Queries
│   ├── Countries/
│   └── Brands/
├── Controllers/          # API Controllers
├── Common/
│   ├── Exceptions/       # Custom exceptions
│   ├── Behaviors/        # MediatR pipeline behaviors
│   └── Middleware/       # Global exception handling
└── Program.cs           # Application entry point
```

### Key Patterns

1. **CQRS Pattern**: Separate read and write operations using MediatR
2. **Repository Pattern**: Abstract data access with clean interfaces
3. **Mapper Pattern**: Static methods for entity/DTO conversion
4. **Pipeline Behaviors**: Validation and logging via MediatR

## Database Schema

### Countries Table
- `Id` (int, PK)
- `Name` (nvarchar(100), Required)
- `IsoCode` (nvarchar(3), Required, Unique)
- `CreatedDate` (datetime, Required)
- `UpdatedDate` (datetime, Required)

### Brands Table
- `Id` (int, PK)
- `Name` (nvarchar(100), Required)
- `Description` (nvarchar(500), Optional)
- `CountryId` (int, FK, Required)
- `CreatedDate` (datetime, Required)
- `UpdatedDate` (datetime, Required)

## API Endpoints

### Countries
- `GET /api/country` - Get all countries
- `GET /api/country/{id}` - Get country by ID
- `POST /api/country` - Create new country
- `PUT /api/country/{id}` - Update country
- `DELETE /api/country/{id}` - Delete country

### Brands
- `GET /api/brand` - Get all brands
- `GET /api/brand/{id}` - Get brand by ID
- `GET /api/brand/country/{countryId}` - Get brands by country
- `POST /api/brand` - Create new brand
- `PUT /api/brand/{id}` - Update brand
- `DELETE /api/brand/{id}` - Delete brand

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server or SQL Server LocalDB

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/SharanJNegali/BrandCountryManager.git
   cd BrandCountryManager
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Update connection string in `appsettings.json` if needed

4. Build the project:
   ```bash
   dotnet build
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

6. Navigate to `https://localhost:5001` to access the Swagger UI

### Database Setup

The application uses Entity Framework Code First with automatic database creation. The database will be created automatically when you first run the application, complete with seed data.

**Seed Data Includes:**
- 4 Countries: USA, UK, Germany, Japan
- 4 Brands: Apple, Microsoft, BMW, Toyota

## Sample API Usage

### Create a Country
```json
POST /api/country
{
  "name": "France",
  "isoCode": "FRA"
}
```

### Create a Brand
```json
POST /api/brand
{
  "name": "Renault",
  "description": "French automotive manufacturer",
  "countryId": 5
}
```

### Get All Countries
```json
GET /api/country

Response:
[
  {
    "id": 1,
    "name": "United States",
    "isoCode": "USA",
    "createdDate": "2024-01-01T10:00:00Z",
    "updatedDate": "2024-01-01T10:00:00Z"
  }
]
```

## Features

### Validation
- Input validation using FluentValidation
- Custom validation rules for business logic
- Automatic validation via MediatR pipeline

### Error Handling
- Global exception middleware
- Structured error responses
- Proper HTTP status codes

### Logging
- Comprehensive logging using built-in .NET logging
- Request/response logging via MediatR behavior
- Performance monitoring

### Documentation
- Swagger/OpenAPI integration
- XML documentation comments
- Interactive API documentation

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server
- **CQRS**: MediatR 12.x
- **Validation**: FluentValidation 11.x
- **Documentation**: Swashbuckle (Swagger)
- **Logging**: Microsoft.Extensions.Logging

## Development Notes

### Adding New Features
1. Create entity in `Models/Entities/`
2. Create DTOs in `Models/DTOs/`
3. Add repository interface and implementation
4. Create mapper methods
5. Implement CQRS commands and queries
6. Add controller endpoints
7. Add validation rules

### Testing
- Unit tests should focus on business logic in handlers
- Integration tests should test complete API workflows
- Repository tests should verify data access patterns

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request
