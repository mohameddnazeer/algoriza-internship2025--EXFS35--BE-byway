using Byway.Api.Models;

namespace Byway.Api.Services;

public class CatalogService
{
    public List<Category> Categories { get; } =
    [
        new Category { Id = 1, Name = "Fullstack Development", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Category { Id = 2, Name = "Backend Development", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Category { Id = 3, Name = "Frontend Development", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Category { Id = 4, Name = "UX/UI Design", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Category { Id = 5, Name = "Mobile Development", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Category { Id = 6, Name = "Data Science", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Category { Id = 7, Name = "DevOps & Cloud", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Category { Id = 8, Name = "Cybersecurity", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
    ];

    public List<Instructor> Instructors { get; } =
    [
        new Instructor { Id = 1, Name = "Mona Ali", JobTitle = JobTitle.FullstackDeveloper, Bio = "10+ years experience in full-stack development with expertise in React, Node.js, and cloud technologies.", Email = "mona.ali@byway.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Instructor { Id = 2, Name = "Omar Hassan", JobTitle = JobTitle.BackendDeveloper, Bio = "Senior backend engineer specializing in APIs, microservices, and database optimization.", Email = "omar.hassan@byway.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Instructor { Id = 3, Name = "Sara Nabil", JobTitle = JobTitle.FrontendDeveloper, Bio = "Frontend specialist with deep expertise in React, TypeScript, and modern web technologies.", Email = "sara.nabil@byway.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Instructor { Id = 4, Name = "Ahmed Mahmoud", JobTitle = JobTitle.UXUIDesigner, Bio = "UX/UI designer with 8+ years creating intuitive and beautiful user experiences.", Email = "ahmed.mahmoud@byway.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Instructor { Id = 5, Name = "Fatima Al-Zahra", JobTitle = JobTitle.FullstackDeveloper, Bio = "Mobile and web developer with expertise in React Native, Flutter, and cross-platform development.", Email = "fatima.alzahra@byway.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Instructor { Id = 6, Name = "Youssef Khaled", JobTitle = JobTitle.BackendDeveloper, Bio = "Data scientist and machine learning engineer with PhD in Computer Science.", Email = "youssef.khaled@byway.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Instructor { Id = 7, Name = "Layla Mansour", JobTitle = JobTitle.FullstackDeveloper, Bio = "DevOps engineer and cloud architect with expertise in AWS, Docker, and Kubernetes.", Email = "layla.mansour@byway.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Instructor { Id = 8, Name = "Karim Farouk", JobTitle = JobTitle.BackendDeveloper, Bio = "Cybersecurity expert with 12+ years in ethical hacking and security architecture.", Email = "karim.farouk@byway.com", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
    ];

    public List<Course> Courses { get; } =
    [
        // Frontend Development Courses
        new Course { Id = 1, Name = "React Fundamentals", Slug = "react-fundamentals", CategoryId = 3, InstructorId = 3, Level = Level.Beginner, Price = 49.99m, Rating = 4.7m, Description = "Learn React from scratch with hands-on projects and modern best practices.", Duration = 120, StudentsCount = 1250, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Course { Id = 2, Name = "Advanced React & TypeScript", Slug = "advanced-react-typescript", CategoryId = 3, InstructorId = 3, Level = Level.Expert, Price = 89.99m, Rating = 4.9m, Description = "Master advanced React patterns, hooks, and TypeScript integration.", Duration = 180, StudentsCount = 890, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Course { Id = 3, Name = "Vue.js Complete Guide", Slug = "vuejs-complete-guide", CategoryId = 3, InstructorId = 1, Level = Level.Intermediate, Price = 59.99m, Rating = 4.6m, Description = "Comprehensive Vue.js course covering Vue 3, Composition API, and Vuex.", Duration = 150, StudentsCount = 720, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        
        // Backend Development Courses
        new Course { Id = 4, Name = "ASP.NET Core Web API", Slug = "aspnet-core-webapi", CategoryId = 2, InstructorId = 2, Level = Level.Intermediate, Price = 69.99m, Rating = 4.5m, Description = "Build robust REST APIs with ASP.NET Core, Entity Framework, and authentication.", Duration = 200, StudentsCount = 950, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Course { Id = 5, Name = "Node.js & Express Masterclass", Slug = "nodejs-express-masterclass", CategoryId = 2, InstructorId = 1, Level = Level.Intermediate, Price = 64.99m, Rating = 4.8m, Description = "Complete Node.js backend development with Express, MongoDB, and JWT authentication.", Duration = 220, StudentsCount = 1100, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Course { Id = 6, Name = "Python Django for Beginners", Slug = "python-django-beginners", CategoryId = 2, InstructorId = 6, Level = Level.Beginner, Price = 54.99m, Rating = 4.4m, Description = "Learn web development with Python Django framework from basics to deployment.", Duration = 160, StudentsCount = 680, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        
        // Fullstack Development Courses
        new Course { Id = 7, Name = "Fullstack MERN Development", Slug = "fullstack-mern-development", CategoryId = 1, InstructorId = 1, Level = Level.Expert, Price = 99.99m, Rating = 4.8m, Description = "Complete MERN stack development: MongoDB, Express, React, and Node.js.", Duration = 300, StudentsCount = 1500, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Course { Id = 8, Name = "Next.js Full-Stack App", Slug = "nextjs-fullstack-app", CategoryId = 1, InstructorId = 3, Level = Level.Intermediate, Price = 79.99m, Rating = 4.7m, Description = "Build modern full-stack applications with Next.js, Prisma, and PostgreSQL.", Duration = 250, StudentsCount = 820, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        
        // UX/UI Design Courses
        new Course { Id = 9, Name = "UX Design Fundamentals", Slug = "ux-design-fundamentals", CategoryId = 4, InstructorId = 4, Level = Level.Beginner, Price = 39.99m, Rating = 4.6m, Description = "Learn user experience design principles, research methods, and prototyping.", Duration = 100, StudentsCount = 950, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Course { Id = 10, Name = "UI Design with Figma", Slug = "ui-design-figma", CategoryId = 4, InstructorId = 4, Level = Level.Intermediate, Price = 49.99m, Rating = 4.8m, Description = "Master UI design using Figma with design systems and component libraries.", Duration = 120, StudentsCount = 1200, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        
        // Mobile Development Courses
        new Course { Id = 11, Name = "React Native Mobile Apps", Slug = "react-native-mobile-apps", CategoryId = 5, InstructorId = 5, Level = Level.Intermediate, Price = 74.99m, Rating = 4.5m, Description = "Build cross-platform mobile apps with React Native and Expo.", Duration = 180, StudentsCount = 650, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Course { Id = 12, Name = "Flutter Development", Slug = "flutter-development", CategoryId = 5, InstructorId = 5, Level = Level.Beginner, Price = 69.99m, Rating = 4.7m, Description = "Create beautiful mobile apps for iOS and Android using Flutter and Dart.", Duration = 200, StudentsCount = 780, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        
        // Data Science Courses
        new Course { Id = 13, Name = "Python for Data Science", Slug = "python-data-science", CategoryId = 6, InstructorId = 6, Level = Level.Beginner, Price = 59.99m, Rating = 4.6m, Description = "Learn data analysis and visualization with Python, Pandas, and Matplotlib.", Duration = 160, StudentsCount = 1100, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Course { Id = 14, Name = "Machine Learning Fundamentals", Slug = "machine-learning-fundamentals", CategoryId = 6, InstructorId = 6, Level = Level.Intermediate, Price = 89.99m, Rating = 4.8m, Description = "Introduction to machine learning algorithms and practical implementation.", Duration = 220, StudentsCount = 890, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        
        // DevOps & Cloud Courses
        new Course { Id = 15, Name = "Docker & Kubernetes", Slug = "docker-kubernetes", CategoryId = 7, InstructorId = 7, Level = Level.Intermediate, Price = 79.99m, Rating = 4.7m, Description = "Master containerization with Docker and orchestration with Kubernetes.", Duration = 180, StudentsCount = 720, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Course { Id = 16, Name = "AWS Cloud Practitioner", Slug = "aws-cloud-practitioner", CategoryId = 7, InstructorId = 7, Level = Level.Beginner, Price = 64.99m, Rating = 4.5m, Description = "Get started with Amazon Web Services and cloud computing fundamentals.", Duration = 140, StudentsCount = 980, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        
        // Cybersecurity Courses
        new Course { Id = 17, Name = "Ethical Hacking Basics", Slug = "ethical-hacking-basics", CategoryId = 8, InstructorId = 8, Level = Level.Beginner, Price = 69.99m, Rating = 4.6m, Description = "Learn ethical hacking techniques and cybersecurity fundamentals.", Duration = 160, StudentsCount = 650, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new Course { Id = 18, Name = "Web Application Security", Slug = "web-application-security", CategoryId = 8, InstructorId = 8, Level = Level.Intermediate, Price = 84.99m, Rating = 4.8m, Description = "Secure web applications against common vulnerabilities and attacks.", Duration = 200, StudentsCount = 540, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
    ];

    public IEnumerable<Course> MoreLikeThis(int courseId)
    {
        var current = Courses.FirstOrDefault(c => c.Id == courseId);
        if (current is null) return Enumerable.Empty<Course>();
        return Courses.Where(c => c.CategoryId == current.CategoryId && c.Id != courseId)
                      .OrderByDescending(c => c.Id).Take(4);
    }
}
