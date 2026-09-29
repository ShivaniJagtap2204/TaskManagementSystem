# Task Management System

## About the Project

Task Management System is a web-based application developed to manage users, teams, team members, tasks, comments and notifications from one place.

The project consists of an ASP.NET Core Web API for backend operations and an ASP.NET Core MVC application for the frontend. SQL Server is used as the database.

The main purpose of this application is to make task creation, assignment and tracking easier for teams.

## Features

- User management
- Role management
- Team management
- Team member management
- Task management
- Task assignment
- Task priority and status management
- Comments management
- Notifications management
- JWT authentication
- Role-based authorization
- Dashboard
- CRUD operations through Web API
- MVC based frontend
- Swagger API testing

## Technologies Used

### Backend
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt Password Hashing

### Frontend
- ASP.NET Core MVC
- Razor Views
- HTML
- CSS
- Bootstrap
- JavaScript

### Tools
- Visual Studio
- SQL Server Management Studio
- Swagger
- Postman
- Git
- GitHub

## Project Structure

The solution is divided into separate projects to keep the code organized.

- MODEL
  - User
  - Role
  - Team
  - TeamMember
  - TaskItem
  - Comment
  - Notification

- DAL
  - ApplicationDbContext

- TaskManagement.API
  - Controllers
  - Program.cs
  - appsettings.json

- TaskManagement.Web
  - Controllers
  - Views
  - Models
  - wwwroot

## Main Modules

### Users

The Users module is used to create, view, update and delete users. Each user contains information such as name, email, password, role and active status.

### Roles

Roles are used to define the access level of users in the application.

### Teams

The Teams module is used to create and manage teams. A team can have a team name and manager.

### Team Members

The Team Members module is used to associate users with teams.

### Tasks

The Tasks module is the main part of the application. Tasks contain information such as title, description, assigned user, assigned by user, team, priority, status and deadline.

Users can create, view, update and delete tasks.

### Comments

Comments can be created against tasks and are associated with users.

### Notifications

Notifications are maintained for users and can also be associated with tasks.

## Authentication

The application uses JWT based authentication.

When a user logs in successfully, the API generates a JWT token. The MVC application stores the token in session and sends the token with protected API requests.

Protected API endpoints use the `[Authorize]` attribute.

Admin-level operations use role-based authorization such as:

`[Authorize(Roles = "1")]`

## Password Security

User passwords are handled using BCrypt hashing instead of storing passwords directly.

During registration or user creation, the password is converted into a secure hash. During login, the entered password is verified against the stored hash.

## Database

SQL Server is used as the database.

The database connection is configured in the `appsettings.json` file of the API project.

Example connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=TaskManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

Update the connection string according to the local SQL Server configuration.

## API Endpoints

The main API endpoints are:

Users:
- GET `/api/Users`
- POST `/api/Users`
- PUT `/api/Users/{id}`
- DELETE `/api/Users/{id}`
- POST `/api/Users/login`

Roles:
- GET `/api/Roles`
- POST `/api/Roles`
- PUT `/api/Roles/{id}`
- DELETE `/api/Roles/{id}`

Teams:
- GET `/api/Teams`
- POST `/api/Teams`
- PUT `/api/Teams/{id}`
- DELETE `/api/Teams/{id}`

Tasks:
- GET `/api/Tasks`
- POST `/api/Tasks`
- PUT `/api/Tasks/{id}`
- DELETE `/api/Tasks/{id}`

Comments:
- GET `/api/Comments`
- POST `/api/Comments`
- PUT `/api/Comments/{id}`
- DELETE `/api/Comments/{id}`

Notifications:
- GET `/api/Notifications`
- POST `/api/Notifications`
- PUT `/api/Notifications/{id}`
- DELETE `/api/Notifications/{id}`

## How to Run the Project

1. Open the solution in Visual Studio.

2. Configure the SQL Server connection string in:

`TaskManagement.API/appsettings.json`

3. Build the solution and make sure there are no build errors.

4. Run the `TaskManagement.API` project.

5. Open Swagger and check the available API endpoints.

6. Run the `TaskManagement.Web` project.

7. Login using a valid user account.

8. After successful login, the user is redirected to the dashboard.

9. From the dashboard, users can access Users, Teams, Team Members, Tasks, Comments and Notifications.

## Application Flow

User Login → MVC Application → Web API → JWT Token → Session → Dashboard → Application Modules → SQL Server

## Dashboard

The dashboard provides a basic overview of the system and navigation to the main modules.

The dashboard contains:

- Users
- Teams
- Team Members
- Tasks
- Comments
- Notifications

## Security

The application uses JWT authentication to protect API resources.

Authentication and authorization are handled separately. Authenticated users can access protected resources, while specific operations can be restricted based on the user's role.

The MVC application also clears the session during logout.

## Future Improvements

The application can be extended with:

- Task search and filtering
- Pagination
- Task progress tracking
- Email notifications
- File attachments
- Better reporting
- More detailed role and permission management
- Improved dashboard charts
- Improved responsive UI


## NuGet Packages

The following NuGet packages are used in the project:

### TaskManagement.API

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.IdentityModel.Tokens
- Swashbuckle.AspNetCore
- BCrypt.Net-Next

### TaskManagement.Web

- Microsoft.AspNetCore.Mvc
- Microsoft.AspNetCore.Session
- Microsoft.Extensions.Http

These packages are used for database connectivity, Entity Framework Core, JWT authentication, API documentation with Swagger, password hashing, session management and HTTP communication between the MVC application and Web API.


## Author

Task Management System developed using ASP.NET Core Web API, ASP.NET Core MVC, C#, Entity Framework Core and SQL Server.


