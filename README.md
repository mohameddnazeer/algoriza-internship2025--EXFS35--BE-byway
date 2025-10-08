# Byway  API

A modern  API built with ASP.NET Core 9.0, Entity Framework Core, and SQLite. This RESTful API provides comprehensive functionality for managing courses, users, authentication, shopping cart, and more.



## 📄 License to Login

login by admin role => admin@byway.com for mail and Admin123! for password
login by user role => mohamed55nazeer55@gmail.com for mail and Nazeer@123 for password


## 🚀 Features

- **User Authentication & Authorization** - JWT-based authentication system
- **Course Management** - Full CRUD operations for courses and categories
- **Shopping Cart** - Add, remove, and manage cart items
- **User Profiles** - User registration, login, and profile management
- **Instructor Management** - Dedicated instructor profiles and course association
- **File Upload** - Image upload functionality with secure file handling
- **Admin Dashboard** - Administrative controls and analytics
- **Database Migrations** - Version-controlled database schema management

## 🛠 Technology Stack

- **Framework**: ASP.NET Core 9.0
- **Database**: SQLite with Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Password Hashing**: BCrypt.NET
- **API Documentation**: Swagger/OpenAPI
- **File Storage**: Local file system with organized uploads

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Git (for version control)
- Visual Studio 2022 or VS Code (recommended)

## 🔧 Installation & Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd byway-starter/byway/server/src/Byway.Api
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Database Setup
The project uses SQLite with Entity Framework Core. The database will be created automatically on first run.

```bash
# Apply database migrations
dotnet ef database update
```

### 4. Configuration
Update `appsettings.json` with your preferred settings:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=BywayDb.db"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "BywayApi",
    "Audience": "BywayClient",
    "ExpirationInMinutes": 1440
  }
}
```

### 5. Run the Application
```bash
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:7000` (or configured port)
- HTTP: `http://localhost:5000` (or configured port)
- Swagger UI: `https://localhost:7000/swagger`

## 📚 API Endpoints

### Authentication
- `POST /api/v1/auth/register` - User registration
- `POST /api/v1/auth/login` - User login
- `GET /api/v1/auth/profile` - Get user profile
- `PUT /api/v1/auth/profile` - Update user profile

### Courses
- `GET /api/v1/courses` - Get all courses
- `GET /api/v1/courses/{id}` - Get course by ID
- `POST /api/v1/courses` - Create new course
- `PUT /api/v1/courses/{id}` - Update course
- `DELETE /api/v1/courses/{id}` - Delete course

### Cart Management
- `GET /api/v1/cart` - Get user's cart
- `POST /api/v1/cart` - Add item to cart
- `PUT /api/v1/cart/{id}` - Update cart item
- `DELETE /api/v1/cart/{id}` - Remove item from cart

### Catalog
- `GET /api/v1/catalog/categories` - Get all categories
- `GET /api/v1/catalog/featured` - Get featured courses
- `GET /api/v1/catalog/search` - Search courses

### File Upload
- `POST /api/v1/upload/image` - Upload image files

### Admin
- `GET /api/v1/admin/dashboard` - Admin dashboard data
- `GET /api/v1/admin/users` - Manage users
- `GET /api/v1/admin/orders` - Manage orders

## 📁 Project Structure

```
Byway.Api/
├── Controllers/          # API Controllers
│   ├── AuthController.cs
│   ├── CourseController.cs
│   ├── CartController.cs
│   └── ...
├── Data/                 # Database Context
│   └── BywayDbContext.cs
├── Models/               # Data Models
│   ├── User.cs
│   ├── Course.cs
│   ├── CartItem.cs
│   └── DTOs/            # Data Transfer Objects
├── Services/            # Business Logic
│   ├── JwtService.cs
│   ├── CartService.cs
│   └── CatalogService.cs
├── Migrations/          # EF Core Migrations
├── uploads/             # File Upload Directory
└── wwwroot/            # Static Files
```

## 🔐 Authentication

The API uses JWT (JSON Web Tokens) for authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your-jwt-token>
```

## 📊 Database Schema

The application includes the following main entities:
- **Users** - User accounts and profiles
- **Courses** - Course information and metadata
- **Categories** - Course categorization
- **Instructors** - Instructor profiles
- **CartItems** - Shopping cart functionality
- **Orders** - Order management
- **Payments** - Payment processing

## 🚀 Deployment

### Development
```bash
dotnet run --environment Development
```

### Production
```bash
dotnet publish -c Release -o ./publish
cd publish
dotnet Byway.Api.dll
```

### Docker (Optional)
Create a `Dockerfile` for containerized deployment:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Byway.Api.csproj", "./"]
RUN dotnet restore "Byway.Api.csproj"
COPY . .
RUN dotnet build "Byway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Byway.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Byway.Api.dll"]
```

## 🧪 Testing

Run the test suite:
```bash
dotnet test
```

## 📝 Development Guidelines

1. **Code Style**: Follow C# coding conventions
2. **Database Changes**: Always create migrations for schema changes
3. **API Versioning**: Use versioned endpoints (`/api/v1/`)
4. **Error Handling**: Implement proper exception handling
5. **Security**: Validate input data and implement proper authorization

## 🔧 Common Commands

```bash
# Add new migration
dotnet ef migrations add <MigrationName>

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# Generate entity from existing database
dotnet ef dbcontext scaffold "Data Source=BywayDb.db" Microsoft.EntityFrameworkCore.Sqlite
```



## 🎯 Roadmap

- [ ] Add comprehensive unit tests
- [ ] Implement Redis caching
- [ ] Add email notifications
- [ ] Implement payment gateway integration
- [ ] Add real-time features with SignalR
- [ ] Implement advanced search functionality
- [ ] Add API rate limiting
- [ ] Implement comprehensive logging

---

**Built with ❤️ using ASP.NET Core**
