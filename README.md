# \# Product Management API

# 

# A production-style RESTful Web API built with \*\*ASP.NET Core 8\*\* following \*\*Clean Architecture\*\* principles. This project demonstrates authentication, authorization, CRUD operations, API versioning, logging, testing, and containerization using industry best practices.

# 

# \---

# 

# \## 🚀 Features

# 

# \- RESTful Product CRUD API

# \- ASP.NET Core Identity

# \- JWT Authentication

# \- Refresh Token Strategy

# \- API Versioning

# \- Global Exception Handling Middleware

# \- FluentValidation

# \- AutoMapper

# \- Repository Pattern

# \- Unit of Work Pattern

# \- Entity Framework Core

# \- SQL Server

# \- Serilog Structured Logging

# \- Swagger / OpenAPI Documentation

# \- Unit Testing (xUnit + Moq + FluentAssertions)

# \- Integration Testing (WebApplicationFactory)

# \- Docker \& Docker Compose

# 

# \---

# 

# \# 🏗 Architecture

# 

# The project follows \*\*Clean Architecture\*\*.

# 

# ```

# Solution

# │

# ├── src

# │   ├── ProductManagement.API

# │   ├── ProductManagement.Application

# │   ├── ProductManagement.Domain

# │   └── ProductManagement.Infrastructure

# │

# ├── tests

# │   ├── ProductManagement.Application.Tests

# │   ├── ProductManagement.Infrastructure.Tests

# │   └── ProductManagement.IntegrationTests

# │

# ├── Dockerfile

# └── docker-compose.yml

# ```

# 

# \---

# 

# \# 🛠 Tech Stack

# 

# | Technology | Version |

# |------------|---------|

# | .NET | 8 |

# | ASP.NET Core Web API | 8 |

# | Entity Framework Core | 8 |

# | SQL Server | 2022 |

# | ASP.NET Core Identity | Latest |

# | JWT | Bearer Authentication |

# | AutoMapper | Latest |

# | FluentValidation | Latest |

# | Serilog | Latest |

# | Swagger | Swashbuckle |

# | xUnit | Latest |

# | Moq | Latest |

# | FluentAssertions | Latest |

# | Docker | Latest |

# 

# \---

# 

# \# 📂 Project Structure

# 

# \## API

# 

# \- Controllers

# \- Middleware

# \- Services

# \- Swagger Configuration

# \- Program.cs

# 

# \## Application

# 

# \- DTOs

# \- Interfaces

# \- Services

# \- Validators

# \- Mapping Profiles

# \- Constants

# 

# \## Domain

# 

# \- Entities

# \- Exceptions

# 

# \## Infrastructure

# 

# \- DbContext

# \- Entity Configurations

# \- Repository

# \- Unit of Work

# \- Identity

# \- Authentication Services

# 

# \## Tests

# 

# \- Unit Tests

# \- Integration Tests

# 

# \---

# 

# \# 🔐 Authentication

# 

# Authentication is implemented using \*\*ASP.NET Core Identity\*\* and \*\*JWT Bearer Tokens\*\*.

# 

# \## Flow

# 

# ```

# Register

# &#x20;     ↓

# Login

# &#x20;     ↓

# Access Token + Refresh Token

# &#x20;     ↓

# Access Protected APIs

# &#x20;     ↓

# Refresh Token

# &#x20;     ↓

# Generate New Access Token

# ```

# 

# \---

# 

# \# 🔑 Authorization

# 

# Protected endpoints require JWT authentication.

# 

# Example:

# 

# ```

# Authorization: Bearer <access\_token>

# ```

# 

# \---

# 

# \# 🌐 API Versioning

# 

# API Versioning is implemented using URL versioning.

# 

# Example:

# 

# ```

# /api/v1/Auth/login

# 

# /api/v1/Products

# ```

# 

# \---

# 

# \# 📘 Swagger

# 

# Swagger is configured with JWT authentication.

# 

# Features

# 

# \- API Versioning

# \- JWT Authorization

# \- Request/Response Models

# 

# \---

# 

# \# 📦 Product Endpoints

# 

# \## Authentication

# 

# ```

# POST /api/v1/Auth/register

# 

# POST /api/v1/Auth/login

# 

# POST /api/v1/Auth/refresh-token

# ```

# 

# \## Products

# 

# ```

# GET /api/v1/Products

# 

# GET /api/v1/Products/{id}

# 

# POST /api/v1/Products

# 

# PUT /api/v1/Products/{id}

# 

# DELETE /api/v1/Products/{id}

# ```

# 

# \---

# 

# \# 🗄 Database

# 

# Database: SQL Server

# 

# Main Tables

# 

# \- AspNetUsers

# \- AspNetRoles

# \- Product

# \- Item

# \- RefreshToken

# 

# Entity Framework Core Code First with Migrations is used.

# 

# \---

# 

# \# ✅ Validation

# 

# Request validation is implemented using \*\*FluentValidation\*\*.

# 

# Example validations

# 

# \- Product Name Required

# \- Email Validation

# \- Password Validation

# 

# \---

# 

# \# ⚠ Global Exception Handling

# 

# A custom middleware handles exceptions globally and returns consistent JSON error responses.

# 

# Example

# 

# ```json

# {

# &#x20; "statusCode": 404,

# &#x20; "message": "Product with Id 10 was not found."

# }

# ```

# 

# \---

# 

# \# 📋 Logging

# 

# Structured logging is implemented using \*\*Serilog\*\*.

# 

# Logs are written to:

# 

# ```

# Logs/

# ```

# 

# Features

# 

# \- Daily Rolling Files

# \- Console Logging

# \- Exception Logging

# 

# \---

# 

# \# 🧪 Testing

# 

# \## Unit Tests

# 

# Implemented using

# 

# \- xUnit

# \- Moq

# \- FluentAssertions

# 

# Covered Scenarios

# 

# \- Create Product

# \- Get Product

# \- Get All Products

# \- Update Product

# \- Delete Product

# \- NotFound Exceptions

# 

# \## Integration Tests

# 

# Implemented using

# 

# \- WebApplicationFactory

# \- HttpClient

# 

# Covered APIs

# 

# \- Register

# \- Login

# \- Products API

# 

# \---

# 

# \# 🐳 Docker

# 

# The application is fully containerized.

# 

# Containers

# 

# \- ProductManagement.API

# \- SQL Server

# 

# Run

# 

# ```bash

# docker compose up --build

# ```

# 

# Swagger

# 

# ```

# http://localhost:8080/swagger

# ```

# 

# \---

# 

# \# ▶ Running Locally

# 

# \## Clone

# 

# ```bash

# git clone https://github.com/<your-username>/ProductManagement.git

# ```

# 

# \## Restore Packages

# 

# ```bash

# dotnet restore

# ```

# 

# \## Apply Migrations

# 

# ```bash

# Update-Database

# ```

# 

# \## Run

# 

# ```bash

# dotnet run --project src/ProductManagement.API

# ```

# 

# Swagger

# 

# ```

# https://localhost:7018/swagger

# ```

# 

# \---

# 

# \# 🔒 Security

# 

# \- JWT Authentication

# \- Refresh Token Rotation

# \- Password Hashing using ASP.NET Identity

# \- Authorization using Bearer Tokens

# \- HTTPS Enabled

# 

# \---

# 

# \# 📸 Screenshots

# 

# Add screenshots for:

# 

# \- Swagger UI

# \- Login API

# \- Products API

# \- Docker Containers

# \- Unit Test Results

# \- Integration Test Results

# 

# \---

# 

# \# 🚀 Future Improvements

# 

# \- Role-Based Authorization

# \- Pagination

# \- Searching \& Filtering

# \- Redis Caching

# \- Response Compression

# \- Health Checks

# \- CI/CD Pipeline

# \- Azure Deployment

# 

# \---

# 

# \# 👨‍💻 Author

# 

# Gitesh Lokhande

# 

# Backend Developer | ASP.NET Core | C# | SQL Server | Entity Framework Core

# 

# GitHub: https://github.com/GiteshLokhande

# LinkedIn: https://www.linkedin.com/in/gitesh-lokhande-0a3630222/

