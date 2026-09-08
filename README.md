# User Management API

A simple ASP.NET Core Web API developed for TechHive Solutions as part of a back-end development project.

The API provides CRUD functionality for managing users and includes input validation, exception handling, token authentication, and request/response logging middleware.

## Features

* Create users
* Retrieve all users
* Retrieve a user by ID
* Update users
* Delete users
* Validate user information
* Handle missing users with appropriate HTTP responses
* Handle unhandled exceptions
* Authenticate requests using a bearer token
* Log incoming requests and outgoing responses
* Swagger API documentation

## Technologies

* C#
* ASP.NET Core
* .NET 10
* REST API
* Swagger
* Microsoft Copilot

## API Endpoints

| Method | Endpoint          | Description             |
| ------ | ----------------- | ----------------------- |
| GET    | `/api/users`      | Retrieve all users      |
| GET    | `/api/users/{id}` | Retrieve a user by ID   |
| POST   | `/api/users`      | Create a new user       |
| PUT    | `/api/users/{id}` | Update an existing user |
| DELETE | `/api/users/{id}` | Delete a user           |

## Authentication

The API uses a simple bearer token middleware for demonstration purposes.

Use the following authorization header when calling the API:

```text
Authorization: Bearer techhive-secret-token
```

Requests without a valid token receive a `401 Unauthorized` response.

## Validation

The User model uses data annotation validation.

The API validates:

* Required name
* Name length
* Required email
* Valid email format
* Required department
* Department length

Invalid user data results in a `400 Bad Request` response.

## Middleware

The application uses three custom middleware components.

### Exception Handling Middleware

Catches unhandled exceptions and returns a consistent JSON error response.

### Token Authentication Middleware

Checks the Authorization header and verifies the bearer token before allowing access to API endpoints.

### Request Logging Middleware

Logs:

* HTTP method
* Request path
* Response status code
* Request processing duration

The middleware is configured in the following order:

```text
Exception Handling
        ↓
Authentication
        ↓
Request/Response Logging
        ↓
Controllers
```

## Microsoft Copilot

Microsoft Copilot was used as a coding assistant during the development of the project.

It assisted with:

* Generating the initial CRUD API structure
* Suggesting validation improvements
* Identifying potential error-handling issues
* Creating middleware for logging, authentication, and exception handling
* Reviewing and improving the middleware pipeline
* Helping troubleshoot and optimize the code

The generated suggestions were reviewed, tested, and adapted to meet the requirements of the project.

## Testing

The API can be tested using Swagger, Postman, or another API testing tool.

Example authenticated request:

```text
GET /api/users
Authorization: Bearer techhive-secret-token
```

Expected response:

```text
200 OK
```

An invalid or missing token should return:

```text
401 Unauthorized
```

Requesting a user that does not exist should return:

```text
404 Not Found
```

Invalid user data should return:

```text
400 Bad Request
```
