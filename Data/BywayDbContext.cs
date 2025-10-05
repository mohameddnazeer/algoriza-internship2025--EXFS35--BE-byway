using Microsoft.EntityFrameworkCore;
using Byway.Api.Models;

namespace Byway.Api.Data;

public class BywayDbContext : DbContext
{
    public BywayDbContext(DbContextOptions<BywayDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Category)
            .WithMany(cat => cat.Courses)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Instructor)
            .WithMany(i => i.Courses)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.User)
            .WithMany(u => u.CartItems)
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Course)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Course)
            .WithMany(c => c.OrderItems)
            .HasForeignKey(oi => oi.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Order)
            .WithOne(o => o.Payment)
            .HasForeignKey<Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure unique constraints
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Slug)
            .IsUnique();

        modelBuilder.Entity<CartItem>()
            .HasIndex(ci => new { ci.UserId, ci.CourseId })
            .IsUnique();

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Fullstack", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = 2, Name = "Backend", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = 3, Name = "Frontend", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Id = 4, Name = "UX/UI Design", CreatedAt = seedDate, UpdatedAt = seedDate }
        );

        // Seed Instructors
        modelBuilder.Entity<Instructor>().HasData(
            new Instructor 
            { 
                Id = 1, 
                Name = "John Smith", 
                JobTitle = JobTitle.FullstackDeveloper, 
                Bio = "Experienced fullstack developer with 10+ years in web development",
                Email = "john.smith@byway.com",
                CreatedAt = seedDate, 
                UpdatedAt = seedDate 
            },
            new Instructor 
            { 
                Id = 2, 
                Name = "Sarah Johnson", 
                JobTitle = JobTitle.FrontendDeveloper, 
                Bio = "Frontend specialist focusing on React and modern JavaScript",
                Email = "sarah.johnson@byway.com",
                CreatedAt = seedDate, 
                UpdatedAt = seedDate 
            },
            new Instructor 
            { 
                Id = 3, 
                Name = "Mike Wilson", 
                JobTitle = JobTitle.BackendDeveloper, 
                Bio = "Backend expert with expertise in .NET Core and cloud architecture",
                Email = "mike.wilson@byway.com",
                CreatedAt = seedDate, 
                UpdatedAt = seedDate 
            },
            new Instructor 
            { 
                Id = 4, 
                Name = "Emily Davis", 
                JobTitle = JobTitle.UXUIDesigner, 
                Bio = "UX/UI designer passionate about creating intuitive user experiences",
                Email = "emily.davis@byway.com",
                CreatedAt = seedDate, 
                UpdatedAt = seedDate 
            }
        );

        // Seed Users
        modelBuilder.Entity<User>().HasData(
            // Admin User
            new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@byway.com",
                PasswordHash = "$2a$11$0OLx8aV100UT8jNFaqPO6e58R9ha5Xsuk34pYP0t9vF5vMY/aShPC", // Admin123!
                IsAdmin = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            // Regular User
            new User
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe",
                Email = "user@byway.com",
                PasswordHash = "$2a$11$88JM/LL2Q/5O6ekyUNTf0.2z7XRxKMMPQkuN0VDrQ501Yga.fozZW", // User123!
                IsAdmin = false,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            }
        );

        // Seed Courses
        modelBuilder.Entity<Course>().HasData(
            // Frontend Development Courses
            new Course
            {
                Id = 1,
                Name = "React Fundamentals",
                Slug = "react-fundamentals",
                CategoryId = 3, // Frontend
                InstructorId = 2, // Sarah Johnson
                Level = Level.Beginner,
                Price = 49.99m,
                Rating = 4.7m,
                Description = "Learn React from scratch with hands-on projects and modern best practices.",
                LongDescription = "This comprehensive React course covers everything from basic concepts to advanced patterns. You'll build real-world projects and learn industry best practices.",
                Duration = 120, // 2 hours
                StudentsCount = 1250,
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Course
            {
                Id = 2,
                Name = "Advanced React & TypeScript",
                Slug = "advanced-react-typescript",
                CategoryId = 3, // Frontend
                InstructorId = 2, // Sarah Johnson
                Level = Level.Expert,
                Price = 89.99m,
                Rating = 4.9m,
                Description = "Master advanced React patterns, hooks, and TypeScript integration.",
                LongDescription = "Take your React skills to the next level with advanced patterns, custom hooks, performance optimization, and full TypeScript integration.",
                Duration = 180, // 3 hours
                StudentsCount = 890,
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Course
            {
                Id = 3,
                Name = "Vue.js Complete Guide",
                Slug = "vuejs-complete-guide",
                CategoryId = 3, // Frontend
                InstructorId = 1, // John Smith
                Level = Level.Intermediate,
                Price = 59.99m,
                Rating = 4.6m,
                Description = "Comprehensive Vue.js course covering Vue 3, Composition API, and Vuex.",
                LongDescription = "Master Vue.js 3 with this complete guide covering the Composition API, Vuex state management, and building production-ready applications.",
                Duration = 150, // 2.5 hours
                StudentsCount = 750,
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            // Backend Development Courses
            new Course
            {
                Id = 4,
                Name = "ASP.NET Core Web API",
                Slug = "aspnet-core-webapi",
                CategoryId = 2, // Backend
                InstructorId = 3, // Mike Wilson
                Level = Level.Intermediate,
                Price = 69.99m,
                Rating = 4.5m,
                Description = "Build robust REST APIs with ASP.NET Core, Entity Framework, and authentication.",
                LongDescription = "Learn to build scalable and secure REST APIs using ASP.NET Core, Entity Framework Core, JWT authentication, and modern development practices.",
                Duration = 200, // 3.33 hours
                StudentsCount = 920,
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Course
            {
                Id = 5,
                Name = "Node.js & Express Masterclass",
                Slug = "nodejs-express-masterclass",
                CategoryId = 2, // Backend
                InstructorId = 1, // John Smith
                Level = Level.Intermediate,
                Price = 64.99m,
                Rating = 4.4m,
                Description = "Complete Node.js and Express.js course with MongoDB and authentication.",
                LongDescription = "Build full-featured backend applications with Node.js, Express.js, MongoDB, and implement secure authentication and authorization.",
                Duration = 190, // 3.17 hours
                StudentsCount = 1100,
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            // Fullstack Development Courses
            new Course
            {
                Id = 6,
                Name = "Full Stack MERN Development",
                Slug = "fullstack-mern-development",
                CategoryId = 1, // Fullstack
                InstructorId = 1, // John Smith
                Level = Level.Expert,
                Price = 99.99m,
                Rating = 4.8m,
                Description = "Complete MERN stack course: MongoDB, Express, React, and Node.js.",
                LongDescription = "Master the complete MERN stack by building real-world applications. Learn MongoDB, Express.js, React, and Node.js with modern development practices.",
                Duration = 300, // 5 hours
                StudentsCount = 650,
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            // UX/UI Design Courses
            new Course
            {
                Id = 7,
                Name = "UI/UX Design Fundamentals",
                Slug = "uiux-design-fundamentals",
                CategoryId = 4, // UX/UI Design
                InstructorId = 4, // Emily Davis
                Level = Level.Beginner,
                Price = 54.99m,
                Rating = 4.6m,
                Description = "Learn the fundamentals of user interface and user experience design.",
                LongDescription = "Discover the principles of great UI/UX design, learn design thinking processes, and create user-centered designs that convert.",
                Duration = 140, // 2.33 hours
                StudentsCount = 980,
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new Course
            {
                Id = 8,
                Name = "Advanced Figma for Designers",
                Slug = "advanced-figma-designers",
                CategoryId = 4, // UX/UI Design
                InstructorId = 4, // Emily Davis
                Level = Level.Intermediate,
                Price = 74.99m,
                Rating = 4.7m,
                Description = "Master Figma with advanced techniques, components, and design systems.",
                LongDescription = "Take your Figma skills to the next level with advanced prototyping, component systems, design tokens, and collaborative workflows.",
                Duration = 160, // 2.67 hours
                StudentsCount = 540,
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            }
        );
    }
}