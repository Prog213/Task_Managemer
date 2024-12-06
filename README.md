# Task Management API

## Overview
This API is a task management system built with .NET 8, using Entity Framework Core for database management and AutoMapper for object mapping. The API supports CRUD operations on tasks and allows filtering and pagination.

---

## Features
- User authentication and authorization using JWT.
- CRUD operations for tasks.
- Filtering tasks by status, priority, and due dates.
- Pagination and sorting support.

---

## Setup Instructions

### Prerequisites
1. **.NET SDK**: Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
2. **Database**: Install and configure PostgreSQL.
3. **Development Environment**: Visual Studio, Visual Studio Code, or any editor of your choice.
4. **Package Manager**: Ensure `dotnet-ef` is installed for migrations:
   
```bash
dotnet tool install --global dotnet-ef
```

### Running the Project Locally

1. Clone the Repository
```bash
git clone https://github.com/Prog213/Task_Manager.git
```
2. Navigate to folder
```bash
cd Task_Manager\API
```
3. Update environment variables in `appsettings.Development.json`
```bash
"ConnectionStrings": {
    "DefaultConnection": "User ID=your_user; Password=your_password; Host=localhost; Port=5432; Database=your_database_name"
  },
  "TokenKey": "your_secret_token_key (min 64 characters)" 
```
4. Restore Dependencies
```bash
dotnet restore
```
5. Run the Application
```bash
dotnet watch
```
The API will be available at:

Swagger UI: http://localhost:5000/swagger

You can also test the API using Postman

---
## API Endpoints - User Operations

1. `POST /api/account/register`
   - **Description**: Registers a new user.
   - **Body**: 
     ```json
     {
       "username": "string",
       "email": "string",
       "password": "string"
     }
     ```
   - **Validation Requirements**:
     - **Password**: Must be at least 6 characters long and contain at least one special character (e.g., `!@#$%^&*(),.?`).
   - **Returns**: 
     ```json
     {
       "id": "string",
       "username": "string",
       "email": "string",
       "token": "string"
     }
     ```
   - **Possible Status Codes**:
     - `200 OK`: Registration successful.
     - `400 Bad Request`: Validation failed, invalid input or username/password already exists.
     - `500 Internal Server Error`: Unexpected error occurred.

2. `POST /api/account/login`
   - **Description**: Authenticates a user and returns a JWT token.
   - **Body**: 
     ```json
     {
       "username": "string",
       "password": "string"
     }
   - **Returns**: 
     ```json
     {
       "token": "string"
     }
     ```
   - **Possible Status Codes**:
     - `200 OK`: Authentication successful, token returned.
     - `400 Bad Request`: Validation failed or invalid input.
     - `401 Unauthorized`: Incorrect username or password.
     - `500 Internal Server Error`: Unexpected error occurred.

---
## API Endpoints - Task Operations

1. `GET /api/tasks`
   - **Description**: Retrieves a paginated list of tasks for the authenticated user.
   - **Query Parameters**:
     - `pageNumber` (optional): Page number, default is 1.
     - `pageSize` (optional): Page size, default is 5.
     - `status` (optional): Task status (`Pending`, `InProgress`, `Completed`).
     - `priority` (optional): Task priority (`Low`, `Medium`, `High`).
     - `minDate` (optional): Minimum due date.
     - `maxDate` (optional): Maximum due date.
   - **Authorization**: Bearer token required.
   - **Returns**: 
     ```json
     {
       "items": [
         {
           "id": "string",
           "title": "string",
           "description": "string",
           "dueDate": "2024-12-01T00:00:00Z",
           "status": "string",
           "priority": "string",
           "createdAt": "2024-11-25T00:00:00Z",
           "updatedAt": "2024-12-01T00:00:00Z"
         }
       ],
       "currentPage": 1,
       "totalPages": 3,
       "pageSize": 5,
       "totalCount": 13
     }
     ```
   - **Possible Status Codes**:
     - `200 OK`: Successfully retrieved tasks.
     - `401 Unauthorized`: No or invalid token provided.
     - `500 Internal Server Error`: Unexpected error occurred.

2. `GET /api/tasks/{id}`
   - **Description**: Retrieves a specific task by ID for the authenticated user.
   - **Path Parameters**:
     - `id`: Task ID.
   - **Authorization**: Bearer token required.
   - **Returns**: 
     ```json
     {
       "id": "string",
       "title": "string",
       "description": "string",
       "dueDate": "2024-12-01T00:00:00Z",
       "status": "string",
       "priority": "string",
       "createdAt": "2024-11-25T00:00:00Z",
       "updatedAt": "2024-12-01T00:00:00Z"
     }
     ```
   - **Possible Status Codes**:
     - `200 OK`: Successfully retrieved task.
     - `401 Unauthorized`: No or invalid token provided.
     - `404 Not Found`: Task not found for the user.
     - `500 Internal Server Error`: Unexpected error occurred.

3. `POST /api/tasks`
   - **Description**: Creates a new task for the authenticated user.
   - **Body**:
     ```json
     {
       "title": "string",
       "description": "string",
       "dueDate": "2024-12-01T00:00:00Z",
       "status": "Pending",
       "priority": "Low"
     }
     ```
   - **Validation Requirements**:
     - **Title**: Required.
     - **Status**: Must be one of `Pending`, `InProgress`, `Completed`.
     - **Priority**: Must be one of `Low`, `Medium`, `High`.
   - **Authorization**: Bearer token required.
   - **Returns**: 
     ```json
     {
       "id": "string",
       "title": "string",
       "description": "string",
       "dueDate": "2024-12-01T00:00:00Z",
       "status": "string",
       "priority": "string",
       "createdAt": "2024-11-25T00:00:00Z",
       "updatedAt": "2024-11-25T00:00:00Z"
     }
     ```
   - **Possible Status Codes**:
     - `201 Created`: Task successfully created.
     - `400 Bad Request`: Validation failed or invalid input.
     - `401 Unauthorized`: No or invalid token provided.
     - `500 Internal Server Error`: Unexpected error occurred.

4. `PUT /api/tasks/{id}`
   - **Description**: Updates an existing task for the authenticated user.
   - **Path Parameters**:
     - `id`: Task ID.
   - **Body**:
     ```json
     {
       "title": "string",
       "description": "string",
       "dueDate": "2024-12-01T00:00:00Z",
       "status": "string",
       "priority": "string"
     }
     ```
   - **Validation Requirements**:
     - **Title**: Required.
     - **Status**: Must be one of `Pending`, `InProgress`, `Completed`.
     - **Priority**: Must be one of `Low`, `Medium`, `High`.
   - **Authorization**: Bearer token required.
   - **Returns**: `204 No Content` (No response body).
   - **Possible Status Codes**:
     - `204 No Content`: Task successfully updated.
     - `400 Bad Request`: Validation failed or invalid input.
     - `401 Unauthorized`: No or invalid token provided.
     - `403 Forbidden`: Task doesn't belong to user.
     - `404 Not Found`: Task not found for the user.
     - `500 Internal Server Error`: Unexpected error occurred.

5. `DELETE /api/tasks/{id}`
   - **Description**: Deletes a specific task by ID for the authenticated user.
   - **Path Parameters**:
     - `id`: Task ID.
   - **Authorization**: Bearer token required.
   - **Returns**: `204 No Content` (No response body).
   - **Possible Status Codes**:
     - `204 No Content`: Task successfully deleted.
     - `401 Unauthorized`: No or invalid token provided.
     - `403 Forbidden`: Task doesn't belong to user.
     - `404 Not Found`: Task not found for the user.
     - `500 Internal Server Error`: Unexpected error occurred.

## Architecture and Design Choices

### Overview
The application follows the principles of **Clean Architecture** to ensure maintainability, testability, and scalability. The design separates responsibilities into distinct layers, enabling loose coupling between components and making the codebase easier to extend and test.

### Key Principles and Patterns

1. **Service Layer**:
   - Encapsulates the business logic of the application.
   - Serves as the intermediary between the API controllers and the repository layer.
   - Abstracts database operations and ensures all business rules are centralized.

2. **Repository Pattern**:
   - Implements a clean separation of data access logic from the business logic.
   - Provides an abstraction over data access operations, enabling flexibility in changing the data source without impacting other layers.

3. **Dependency Injection**:
   - All services, repositories, and other components are injected using the built-in DI container in ASP.NET Core.
   - Promotes testability and reduces tight coupling between layers.

4. **Logging**:
   - Critical operations such as user registration, task creation, updates, and deletions are logged.
   - Ensures better traceability and debugging for production and development environments.

5. **Configuration**:
   - Application configurations, such as connection strings and environment-specific settings, are stored in `appsettings.json` and environment variables.
   - Enables easy modification of settings without changing the application code.

### Clean Architecture Layers

1. **API (Presentation Layer)**:
   - Handles HTTP requests and responses.
   - Provides endpoints for interacting with the application, following RESTful principles.
   - Delegates processing logic to the Service layer.

2. **Application Layer**:
   - Contains the business logic, including validation, DTOs, and services.
   - Defines interfaces for repository and service interactions.
   - Coordinates tasks between the API and domain layers.

3. **Domain Layer**:
   - Includes core business models, enums, and specifications.
   - Contains the business rules and logic that are independent of any external frameworks.

4. **Infrastructure Layer**:
   - Handles data access, such as interactions with the database.
   - Implements repository interfaces defined in the Application Layer.
   - Includes configurations for logging, database contexts, and external services.

### Benefits of the Architecture
- **Separation of Concerns**: Each layer has a distinct responsibility.
- **Flexibility**: Enables easy replacement or extension of any layer (e.g., switching databases or frameworks).
- **Testability**: Clean separation makes unit testing each layer more straightforward.
- **Scalability**: Modular design supports scaling specific layers as needed without affecting the rest of the system.

This architectural approach ensures the application is robust, maintainable, and prepared for future growth.

