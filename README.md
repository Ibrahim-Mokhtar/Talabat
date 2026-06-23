# Talabat E-Commerce API

![.NET](https://img.shields.io/badge/.NET-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-blue?style=flat-square)

## Overview

Talabat API is a scalable e-commerce backend application developed using ASP.NET Core and Entity Framework Core.

The project simulates a modern online shopping platform that allows customers to browse products, manage baskets, place orders, and process payments while following industry-standard software architecture and design principles.

The system is designed with maintainability, scalability, and separation of concerns in mind.

---

## Features

### Authentication & Authorization

- User Registration
- User Login
- JWT Authentication
- Role-Based Authorization

### Product Management

- Product Catalog
- Product Types
- Product Brands
- Product Filtering
- Product Sorting
- Product Pagination

### Basket Management

- Create Basket
- Update Basket
- Delete Basket
- Redis Basket Storage

### Orders

- Create Orders
- Order History
- Delivery Methods
- Shipping Information

### Payments

- Payment Processing
- Payment Intent Creation
- Order Payment Workflow

### Error Handling

- Custom Error Responses
- Validation Handling
- Global Exception Middleware

---

## Architecture

The project follows Clean Architecture principles and is structured to maintain clear separation between business logic and infrastructure concerns.

### Layers

| Layer | Responsibility |
|---------|---------|
| Core | Business Logic & Entities |
| Repository | Data Access Layer |
| API | Controllers & Endpoints |
| Infrastructure | Database & External Services |

### Design Principles

- Separation of Concerns
- Dependency Injection
- Repository Pattern
- Specification Pattern
- Clean Architecture
- SOLID Principles

---

## Technology Stack

### Backend

- ASP.NET Core
- C#
- Entity Framework Core
- LINQ
- RESTful APIs

### Database

- SQL Server
- Redis

### Authentication

- JWT Authentication
- ASP.NET Identity

### Development Tools

- Visual Studio
- Postman
- Git
- GitHub

---

## Design Patterns

The project implements several software design patterns:

- Repository Pattern
- Specification Pattern
- Dependency Injection
- Unit of Work Pattern
- Generic Repository Pattern

---

## Project Structure

```text
Talabat
│
├── Talabat.APIs
│
├── Talabat.API.Controllers
│
├── Talabat.Core
│   ├── Talabat.Core.Application
│   ├── Talabat.Core.Application.Abstraction
│   └── Talabat.Core.Domain
│
├── Talabat.Infrastructure
│   ├── Talabat.Infrastructure
│   └── Talabat.Infrastructure.Presistence
│
├── Talabat.Client
│
├── Talabat.Dashboard
│
└── AngularProject1
```

---

## API Modules

### Authentication

- Register
- Login

### Products

- Get Products
- Get Product Details
- Filter Products
- Sort Products

### Basket

- Create Basket
- Update Basket
- Delete Basket

### Orders

- Create Order
- Get Orders
- Get Delivery Methods

### Payments

- Create Payment Intent
- Process Payment

---

## Security

The application implements:

- JWT Authentication
- Role-Based Authorization
- Secure Password Hashing
- Protected Endpoints
- Input Validation

---

## Learning Outcomes

Through this project, I gained practical experience in:

- ASP.NET Core Development
- RESTful API Design
- Entity Framework Core
- SQL Server
- Redis Integration
- Clean Architecture
- Repository Pattern
- Specification Pattern
- Authentication & Authorization
- Software Design Principles

---

## My Contribution

- Developed RESTful APIs using ASP.NET Core.
- Implemented Authentication and Authorization.
- Built Product, Basket, Order, and Payment modules.
- Applied Repository and Specification Patterns.
- Designed and managed database operations.
- Implemented business logic and service layers.
- Participated in testing and debugging.

---

## License

This project was developed for educational purposes as part of a Backend Development Diploma.

---

## Contact

### Ibrahim Mokhtar Ahmed Saad

- LinkedIn: https://linkedin.com/in/ibrahim-mokhtar-16966a378
- GitHub: https://github.com/Ibrahim-Mokhtar
- Email: ibrahim.mokhtar1611@gmail.com
