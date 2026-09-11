# Corporate Expenses API

Backend API developed with **.NET 8**, focused on expense management and designed as a technical code sample demonstrating a clean, maintainable and scalable backend architecture.

## Overview

Corporate Expenses is a REST API that allows authenticated users to manage their expenses.

The project demonstrates:

- RESTful API design
- JWT authentication and authorization
- Domain-Driven Design (DDD)
- Clean Architecture principles
- Separation of concerns
- Entity Framework Core
- Dapper
- SQL Server
- Repository pattern
- Application services / managers
- DTOs
- Asynchronous programming
- Dependency Injection
- EF Core migrations
- Secure password hashing with BCrypt

## Architecture

The solution is divided into four projects:

```text
CorporateExpenses.Api
        ↓
CorporateExpenses.Application
        ↓
CorporateExpenses.Domain

CorporateExpenses.Api
        ↓
CorporateExpenses.Infrastructure