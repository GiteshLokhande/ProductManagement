# Product Management API

A production-style RESTful API built with **ASP.NET Core 8** following **Clean Architecture** principles. This project demonstrates enterprise backend development practices including JWT Authentication, Refresh Token Rotation, Role-Based Authorization, API Versioning, Global Exception Handling, Logging, Testing, Docker, and more.

---

# Features

- Product CRUD Operations
- ASP.NET Core Identity Authentication
- JWT Access Token Authentication
- Refresh Token Rotation
- Role-Based Authorization (Admin/User)
- API Versioning
- Clean Architecture
- Repository Pattern
- Unit of Work Pattern
- Entity Framework Core (Code First)
- SQL Server
- FluentValidation
- AutoMapper
- Global Exception Handling Middleware
- Serilog Structured Logging
- Swagger / OpenAPI Documentation
- Pagination
- Searching
- Sorting
- CORS Configuration
- Response Compression
- Security Headers Middleware
- Basic Domain Events
- Unit Testing
- Integration Testing
- Docker Support

---

# Architecture

The solution follows **Clean Architecture**, separating responsibilities into independent layers.

```
ProductManagement
│
├── src
│   ├── ProductManagement.API
│   ├── ProductManagement.Application
│   ├── ProductManagement.Domain
│   └── ProductManagement.Infrastructure
│
├── tests
│   ├── ProductManagement.Application.Tests
│   ├── ProductManagement.Infrastructure.Tests
│   └── ProductManagement.IntegrationTests
│
├── Dockerfile
└── docker-compose.yml
```

---

# Technology Stack

| Technology | Version |
|------------|---------|
| .NET | 8 |
| ASP.NET Core Web API | 8 |
| Entity Framework Core | 8 |
| SQL Server | 2022 |
| ASP.NET Core Identity | ✔ |
| JWT Authentication | ✔ |
| AutoMapper | ✔ |
| FluentValidation | ✔ |
| Serilog | ✔ |
| Swagger / OpenAPI | ✔ |
| xUnit | ✔ |
| Moq | ✔ |
| FluentAssertions | ✔ |
| Docker | ✔ |

---

# Project Structure

## API

- Controllers
- Middleware
- Swagger Configuration
- Dependency Injection
- Program.cs

## Application

- DTOs
- Interfaces
- Services
- Validators
- AutoMapper Profiles
- Constants

## Domain

- Entities
- Domain Events
- Exceptions

## Infrastructure

- Entity Framework Core
- DbContext
- Repository Pattern
- Unit of Work
- Identity
- JWT Token Generation
- Refresh Token Management
- Event Handlers

## Tests

- Unit Tests
- Integration Tests

---

# Authentication Flow

```
Register User
      │
      ▼
ASP.NET Identity
      │
      ▼
Login
      │
      ▼
Generate JWT + Refresh Token
      │
      ▼
Access Protected APIs
      │
      ▼
Refresh Token
      │
      ▼
Generate New Access Token
```

---

# Authorization

Role-Based Authorization has been implemented using ASP.NET Core Identity Roles.

### Roles

- Admin
- User

### Permissions

| Endpoint | User | Admin |
|----------|:----:|:-----:|
| View Products | ✔ | ✔ |
| Create Product | ✔ | ✔ |
| Update Product | ✖ | ✔ |
| Delete Product | ✖ | ✔ |

---

# API Versioning

API Versioning is implemented using URL versioning.

Example:

```
/api/v1/Auth/login

/api/v1/Products
```

---

# API Endpoints

## Authentication

```
POST    /api/v1/Auth/register

POST    /api/v1/Auth/login

POST    /api/v1/Auth/refresh-token
```

## Products

```
GET     /api/v1/Products

GET     /api/v1/Products/{id}

POST    /api/v1/Products

PUT     /api/v1/Products/{id}

DELETE  /api/v1/Products/{id}
```

---

# Pagination, Searching & Sorting

The Products API supports:

### Pagination

```
GET /api/v1/Products?pageNumber=1&pageSize=10
```

### Searching

```
GET /api/v1/Products?search=laptop
```

### Sorting

```
GET /api/v1/Products?sortBy=ProductName&sortOrder=asc
```

### Combined Example

```
GET /api/v1/Products?pageNumber=1&pageSize=5&search=laptop&sortBy=ProductName&sortOrder=desc
```

---

# Database

SQL Server database using Entity Framework Core Code First.

Main tables:

- AspNetUsers
- AspNetRoles
- AspNetUserRoles
- Product
- Item
- RefreshToken

---

# Validation

Request validation is implemented using **FluentValidation**.

Examples:

- Product Name Required
- Email Validation
- Password Validation

---

# Global Exception Handling

A custom middleware catches unhandled exceptions and returns consistent JSON error responses.

Example:

```json
{
  "statusCode": 404,
  "message": "Product with Id 10 was not found."
}
```

---

# Logging

Structured logging is implemented using **Serilog**.

Features:

- Console Logging
- Rolling File Logs
- Exception Logging

---

# Security

Implemented security features include:

- JWT Authentication
- Refresh Token Rotation
- ASP.NET Identity Password Hashing
- Role-Based Authorization
- HTTPS
- CORS Policy
- Response Compression
- Security Headers

Security headers include:

- X-Content-Type-Options
- X-Frame-Options
- Referrer-Policy
- Content-Security-Policy
- Permissions-Policy

---

# Domain Events

A lightweight Domain Events implementation has been included.

Example:

- ProductCreatedEvent
- ProductCreatedEventHandler

This demonstrates how business events can be raised and handled in a decoupled manner.

---

# Testing

## Unit Tests

Frameworks:

- xUnit
- Moq
- FluentAssertions

Covered Scenarios:

- Create Product
- Get Product
- Get Products
- Update Product
- Delete Product
- Not Found Exceptions

## Integration Tests

Implemented using:

- WebApplicationFactory
- HttpClient

Covered APIs:

- Register
- Login
- Product CRUD

---

# Docker

The application is fully containerized.

Run:

```bash
docker compose up --build
```

Swagger:

```
http://localhost:8080/swagger
```

---

# Running Locally

## Clone Repository

```bash
git clone https://github.com/<YOUR_GITHUB_USERNAME>/ProductManagement.git
```

## Restore Packages

```bash
dotnet restore
```

## Apply Database Migrations

```bash
Update-Database
```

## Run Application

```bash
dotnet run --project src/ProductManagement.API
```

Swagger

```
https://localhost:7018/swagger
```

---

# Performance Considerations

Implemented optimizations:

- AsNoTracking() for read-only queries
- Pagination using Skip() and Take()
- Async/Await throughout
- Response Compression
- Repository Pattern
- Unit of Work

---

# Future Improvements

- Redis Caching
- CQRS + MediatR
- Background Jobs (Hangfire)
- Azure Deployment
- GitHub Actions CI/CD
- Health Checks

---

# Screenshots

Add screenshots here before submitting:

- Swagger UI
- Login API
- JWT Authentication
- Product CRUD
- Docker Containers
- Unit Test Results
- Integration Test Results

---

# Author

# Gitesh Lokhande

# 

# Backend Developer | ASP.NET Core | C# | SQL Server | Entity Framework Core

# 

# GitHub: https://github.com/GiteshLokhande

# LinkedIn: https://www.linkedin.com/in/gitesh-lokhande-0a3630222/