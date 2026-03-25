# Clean API Template | Domain-Driven Design

A **.NET Minimal API template** following **Clean Architecture** and **Domain-Driven Design (DDD)** principles.  
Designed for building **scalable, maintainable, and production-ready APIs** with modern patterns and best practices.

---

## Features

- 🚀 **Minimal API** ready-to-use setup  
- 📊 **Logging** with **Serilog** and **Application Insights** integration  
- 🎯 **Result Pattern** for consistent responses  
- 🏗️ **Clean Architecture** with **Domain-Driven Design** principles  
- 🔒 **Strongly-typed / wrapper types** for high type safety  
- 💾 **Entity Framework Core** with **PostgreSQL** database  
- ⚙️ **Options Pattern** for configuration  
- 🐳 **Docker** and **Docker Compose** ready  
- ⚡ **Global Exception Handling** for uncaught errors  
- 🛡️ **Domain / Business-Level Errors** defined for domain-specific validations  

---

## Getting Started

Follow these steps to get your development environment running:

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)  
- [Docker](https://www.docker.com/)  
- Optional: [PostgreSQL](https://www.postgresql.org/download/) (if not using Docker)  

### Setup `.env` file

A `.env` file is already provided in the **root folder**. **Make sure to populate the values** before running the application locally.

Example `.env` content:

```
POSTGRES_USER=postgres
POSTGRES_PASSWORD=secret
POSTGRES_DB=applicationdb
DatabaseConnectionStrings__ApplicationDb=Host=postgres;Port=5432;Database=applicationdb;Username=postgres;Password=secret
ApplicationInsights__ConnectionString=<appinsights-connectionstring>
AppLogging__SourceName=minimalapi
AppLogging__LogLevel=debug
```

### Run locally with Docker

```bash
docker-compose up --build -d
```

This will start the API along with a PostgreSQL database.

### Access the API

The API will be available at:

```
https://localhost:5001
http://localhost:5000
```

---

## Project Structure

```
src/
├── Application/         # Business logic, use cases, services
├── Domain/              # Entities, value objects, aggregates, domain events
├── Infrastructure/      # DB context, repositories, logging, external services
├── API/                 # Minimal API endpoints
├── Shared/              # Common helpers, result types, wrapper types
docker-compose.yml       # Docker setup
.env                     # Environment variables
```

---

## Patterns & Principles

- **Clean Architecture** → separates API, application, domain, and infrastructure layers  
- **DDD** → entities, value objects, aggregates, domain events  
- **Result Pattern** → consistent success/failure response handling  
- **Strongly-Typed / Wrapper Types** → for safer and more predictable code  
- **Global Exception Handling** → handles all uncaught exceptions with structured error responses  
- **Domain/Business-Level Errors** → custom error types for domain-specific validations  

---

## Logging

- Integrated with **Serilog**  
- Can optionally push logs to **Application Insights**  

```csharp
Log.Information("Application started successfully");
```

---

## Contributing

Contributions are welcome!  
Please open issues or pull requests for bug fixes, enhancements, or new features.

---

## License

This project is licensed under the **MIT License**.

