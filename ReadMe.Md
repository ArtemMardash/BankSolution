# Bank System

This project is a scalable Bank System application, structured as a set of microservices with each service adhering to clean architecture principles. This approach allows each microservice to be modular, maintainable, and independently deployable, while clean architecture within each service keeps the code organized. 

- **Customers Service**: handles all operations related to customer data. This includes creating, reading, and deleting customer information.
- **Accounts Service**: handles all account-related operations for customers. It manages account creation, status, withdraw, and deposit


- **API Layer**: Defines WebAPI and controllers for client interactions.
- **Application Layer**: Manages use cases and application-specific logic, handling requests and processing responses.
- **Domain Layer**: Contains core business logic and domain entities, independent of external frameworks.
- **Infrastructure Layer**: Manages external dependencies and consume messages from other microservices
- **Persistence Layer**: Handles database interactions and data persistence, separated from the domain logic.
- **Background Job (for specific services)**: Executes asynchronous tasks like sending notifications. 

## Table of Contents
- [Technologies](#technologies)

## Technologies

The project utilizes the following technologies and libraries:
- Programming languages: `C#`.
- Frameworks: `.NET 8.0`.
- Database: `PostgreSQL`.
- Message Broker: `RabbitMQ`
- Libraries: `MediatR`, `Microsoft.AspNetCore.OpenApi`, `Microsoft.EntityFrameworkCore.Design`, `Swashbuckle.AspNetCore`, `MassTransit`, `Microsoft.Extensions.DependencyInjection`, `Npgsql.EntityFrameworkCore.PostgreSQL`