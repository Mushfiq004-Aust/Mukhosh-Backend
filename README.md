# Mukhosh

**Mukhosh** (মুখোশ, meaning "mask" in Bengali) is a university experience and review platform designed to help students make more informed decisions when choosing a university.

University websites and admission brochures provide official information, but they don't always show what everyday student life is actually like. Mukhosh aims to provide a space where students can share their experiences, opinions, and updates about their universities.

The platform allows users to create posts, interact through comments, favourite posts, and review universities across multiple categories.

> **Backend:** ASP.NET Core Web API (.NET 8)
> **Database:** Microsoft SQL Server
> **ORM:** Entity Framework Core
> **Authentication:** ASP.NET Core Identity + JWT
> **Frontend:** Angular *(in progress)*

---

## Features

### Authentication & Authorization

* User registration and login
* JWT-based authentication
* ASP.NET Core Identity for user management
* Role-based authorization with `User` and `Admin` roles
* Protected endpoints using `[Authorize]`
* Admin-only operations for selected resources

### Posts

* Create posts
* View all posts
* View an individual post
* Update posts
* Delete posts
* Associate posts with users
* Pagination support for post listings

### Comments

* Add comments to posts
* Retrieve comments
* Update comments
* Delete comments
* Associate comments with their corresponding posts and users

### Favourites

* Favourite posts
* Remove a post from favourites
* Prevent duplicate favourite entries

### University Reviews

Users can review universities using multiple rating categories:

* Environment
* Faculty
* Research Facilities
* Education Quality
* Academic Pressure
* Canteen Food
* Administration
* Extracurricular Activities

Each category is rated out of **5**, along with a written review.

The system also supports:

* One review per user per university
* Database-level uniqueness for user/university reviews
* University review statistics
* Per-category average ratings
* Total review count
* Admin-only university creation

---

## Tech Stack

| Category          | Technology                           |
| ----------------- | ------------------------------------ |
| Framework         | ASP.NET Core Web API (.NET 8)        |
| Language          | C#                                   |
| ORM               | Entity Framework Core                |
| Database          | Microsoft SQL Server                 |
| Authentication    | ASP.NET Core Identity                |
| Authorization     | Role-Based Authorization             |
| Tokens            | JWT                                  |
| API Documentation | Swagger / OpenAPI                    |
| API Testing       | Postman                              |
| Frontend          | Angular + TypeScript *(in progress)* |
| Version Control   | Git / GitHub                         |

---

## Architecture

The backend follows a layered structure to separate HTTP handling, data transformation, and database access.

```text
HTTP Request
     │
     ▼
Controller
     │
     │ receives request
     │ validates input / authorization
     ▼
DTO
     │
     │ request / response data
     ▼
Mapper
     │
     │ DTO ↔ Entity conversion
     ▼
Repository Interface
     │
     ▼
Repository
     │
     │ Entity Framework Core
     ▼
DbContext
     │
     ▼
SQL Server
```

### Why this structure?

The project separates responsibilities so that:

* **Controllers** handle HTTP requests, responses, routing, and authorization.
* **DTOs** define the data expected by API endpoints and the data returned to clients.
* **Mappers** convert between DTOs and database entities.
* **Interfaces** define repository contracts.
* **Repositories** handle database operations through Entity Framework Core.
* **Models** represent the application's database entities.
* **DbContext** manages the Entity Framework Core database connection and entity relationships.

---

## Project Structure

```text
api/
│
├── Controllers/
│   └── HTTP endpoints
│
├── DTOs/
│   └── Request and response models
│
├── Mappers/
│   └── DTO ↔ Entity conversion
│
├── Interfaces/
│   └── Repository contracts
│
├── Repository/
│   └── Database access logic
│
├── Models/
│   └── Entity Framework Core models
│
├── Database/
│   └── ApplicationDbContext
│
├── Migrations/
│   └── EF Core migrations
│
├── Program.cs
│   └── Application startup and dependency injection
│
└── appsettings.json
    └── Application configuration
```

---

## Database

The application uses **Microsoft SQL Server** with **Entity Framework Core**.

Entity relationships include:

```text
User
 │
 ├── Posts
 │    │
 │    ├── Comments
 │    │
 │    └── Favourites
 │
 └── Reviews
        │
        └── University
```

Entity Framework Core migrations are used to create and update the database schema.

---

## Prerequisites

Before running the project, make sure you have:

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Microsoft SQL Server
* SQL Server Management Studio or another SQL client
* Git
* [Postman](https://www.postman.com/downloads/) *(optional)*

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/Mushfiq004-Aust/DotNet_v8_Web_API.git
```

Navigate into the API project:

```bash
cd DotNet_v8_Web_API/api
```

---

### 2. Restore dependencies

```bash
dotnet restore
```

---

### 3. Configure the database

The application requires a SQL Server connection string.

Update the connection string according to your local SQL Server instance.

For example:

```text
Server=YOUR_SERVER;
Database=mukhosh;
Trusted_Connection=True;
TrustServerCertificate=True;
```

Replace `YOUR_SERVER` with your SQL Server instance.

For example:

```text
localhost\SQLEXPRESS
```

or:

```text
DESKTOP-XXXXXXX\SQLEXPRESS
```

> Do not commit real database credentials or JWT secrets to GitHub.

---

### 4. Configure JWT

The application also requires a JWT signing key.

Use a strong, randomly generated secret key and configure it through your local development configuration or user secrets.

For example, with .NET User Secrets:

```bash
dotnet user-secrets init
```

Then configure your connection string and JWT signing key according to the configuration keys used by the application.

Example:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_SERVER;Database=mukhosh;Trusted_Connection=True;TrustServerCertificate=True;"
```

```bash
dotnet user-secrets set "JWT:SigningKey" "YOUR_RANDOM_SECRET_KEY"
```

The exact configuration key names should match the configuration used in `Program.cs` and the application's settings classes.

---

### 5. Install Entity Framework Core CLI tools

If `dotnet ef` is not available on your machine:

```bash
dotnet tool install --global dotnet-ef
```

Verify the installation:

```bash
dotnet ef --version
```

---

### 6. Apply database migrations

Run:

```bash
dotnet ef database update
```

This creates or updates the SQL Server database using the existing Entity Framework Core migrations.

---

### 7. Run the API

Start the application with:

```bash
dotnet run
```

For development with automatic reload:

```bash
dotnet watch run
```

The API will run on the URL shown in the terminal.

For example:

```text
http://localhost:5185
```

Swagger UI will be available at:

```text
http://localhost:5185/swagger
```

The port may differ depending on your local configuration.

---

## Authentication

Mukhosh uses **ASP.NET Core Identity** together with **JWT authentication**.

### Register

```http
POST /api/Auth/register
```

A newly registered account receives the default `User` role.

### Login

```http
POST /api/Auth/login
```

A successful login returns a JWT.

The token can then be sent with protected requests using:

```http
Authorization: Bearer YOUR_JWT_TOKEN
```

### Authorization

Protected endpoints use JWT authentication.

Admin-only endpoints additionally require the `Admin` role.

For example:

```csharp
[Authorize(Roles = "Admin")]
```

---

## API Overview

The main API resources currently include:

| Resource   | Main Operations              |
| ---------- | ---------------------------- |
| Auth       | Register, Login              |
| Post       | Create, Read, Update, Delete |
| Comment    | Create, Read, Update, Delete |
| Favourite  | Add, Remove                  |
| University | Read, Create                 |
| Review     | Create, Read, Update         |

Typical endpoints include:

```text
POST   /api/Auth/register
POST   /api/Auth/login

GET    /api/Post
GET    /api/Post/{id}
POST   /api/Post
PUT    /api/Post/{id}
DELETE /api/Post/{id}

GET    /api/Comment/{id}
POST   /api/Comment
PUT    /api/Comment/{id}
DELETE /api/Comment/{id}

POST   /api/Favourite
DELETE /api/Favourite

GET    /api/University
GET    /api/University/{id}
POST   /api/University

GET    /api/Review/{id}
POST   /api/Review
PUT    /api/Review/{id}
```

> Swagger provides the complete endpoint documentation, request schemas, response schemas, and authorization requirements.

---

## Testing the API

### Swagger

After running the API, open:

```text
http://localhost:5185/swagger
```

Swagger can be used to:

* View available endpoints
* Inspect request and response models
* Send API requests
* Test authenticated endpoints
* Inspect generated OpenAPI documentation

### Postman

The API can also be tested using Postman.

With the API running, the OpenAPI specification is available at:

```text
http://localhost:5185/swagger/v1/swagger.json
```

This specification can be imported into Postman to generate requests for the API.

---

## Database Migrations

Create a new migration after changing the Entity Framework Core models:

```bash
dotnet ef migrations add MigrationName
```

Apply the migration:

```bash
dotnet ef database update
```

For example:

```bash
dotnet ef migrations add AddReviewSystem
dotnet ef database update
```

---

## Current Development Status

### Backend

* [x] ASP.NET Core Web API
* [x] Entity Framework Core
* [x] SQL Server integration
* [x] ASP.NET Core Identity
* [x] JWT authentication
* [x] Role-based authorization
* [x] User management
* [x] Post CRUD
* [x] Comment functionality
* [x] Favourite functionality
* [x] University functionality
* [x] University review system
* [x] Entity Framework Core migrations
* [x] Swagger / OpenAPI documentation
* [x] Postman API testing

### Frontend

* [ ] Angular frontend
* [ ] TypeScript integration
* [ ] Authentication UI
* [ ] Post feed
* [ ] University browsing and filtering
* [ ] University review interface
* [ ] User profile interface

---

## Roadmap

Planned improvements include:

* Angular frontend
* Better post and university search
* Filtering by university and post attributes
* Improved user profiles
* Connecting users and posts to verified universities
* Additional university statistics
* Improved authentication and account management
* Deployment of the backend and frontend
* Production database configuration
* CI/CD pipeline

---

## What I Learned From This Project

This project was built as a practical exercise in developing a structured backend using the ASP.NET ecosystem.

Key areas covered include:

* C# and ASP.NET Core
* REST API development
* Entity Framework Core
* Entity relationships and foreign keys
* SQL Server
* Database migrations
* DTO-based API design
* Repository pattern
* Dependency injection
* ASP.NET Core Identity
* JWT authentication
* Role-based authorization
* API validation
* Swagger / OpenAPI
* API testing with Postman

---

## License

This project currently does not have a separate open-source license.
