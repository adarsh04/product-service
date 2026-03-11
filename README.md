# product-service
This is a product service

Built with .NET

A production-ready .NET API designed with a focus on high performance, distributed caching, and maintainable architecture.

🛠️ Technologies & Tools
This service is built using a modern backend stack to ensure scalability and data integrity:

Framework: .NET 8 / ASP.NET Core

Database: PostgreSQL (via Npgsql.EntityFrameworkCore.PostgreSQL)

Caching: Redis (StackExchangeRedis) for distributed performance

Patterns: Repository Pattern & Dependency Injection for clean separation of concerns

Testing: xUnit & Moq for robust unit testing and mocking

Security: (In Progress) JWT-based authentication and ASP.NET Core Identity

🚀 Getting Started
Follow these steps to get the service up and running on your local machine.

1. Prerequisites
.NET SDK (Version 8.0+)

Docker (Recommended for running Postgres and Redis)

2. Infrastructure Setup
The simplest way to start the required dependencies is via Docker:

Bash
# Start PostgreSQL and Redis
docker run --name product-db -e POSTGRES_PASSWORD=password -p 5432:5432 -d postgres
docker run --name product-cache -p 6379:6379 -d redis
3. Configuration
Update the connection strings in appsettings.Development.json to match your local environment:

JSON
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ProductDb;Username=postgres;Password=password",
    "Redis": "localhost:6379"
  }
}
4. Run the Application
Bash
# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update

# Start the API
dotnet run --project ProductService.Api
🧪 Running Tests
To ensure the service is functioning correctly, run the test suite:

Bash
dotnet test