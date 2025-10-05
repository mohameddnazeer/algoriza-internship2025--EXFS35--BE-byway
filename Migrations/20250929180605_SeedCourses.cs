using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Byway.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Duration", "InstructorId", "IsActive", "Level", "LongDescription", "Name", "Price", "Rating", "Slug", "StudentsCount", "ThumbnailBase64", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Learn React from scratch with hands-on projects and modern best practices.", 120, 2, true, 1, "This comprehensive React course covers everything from basic concepts to advanced patterns. You'll build real-world projects and learn industry best practices.", "React Fundamentals", 49.99m, 4.7m, "react-fundamentals", 1250, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Master advanced React patterns, hooks, and TypeScript integration.", 180, 2, true, 3, "Take your React skills to the next level with advanced patterns, custom hooks, performance optimization, and full TypeScript integration.", "Advanced React & TypeScript", 89.99m, 4.9m, "advanced-react-typescript", 890, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Comprehensive Vue.js course covering Vue 3, Composition API, and Vuex.", 150, 1, true, 2, "Master Vue.js 3 with this complete guide covering the Composition API, Vuex state management, and building production-ready applications.", "Vue.js Complete Guide", 59.99m, 4.6m, "vuejs-complete-guide", 750, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Build robust REST APIs with ASP.NET Core, Entity Framework, and authentication.", 200, 3, true, 2, "Learn to build scalable and secure REST APIs using ASP.NET Core, Entity Framework Core, JWT authentication, and modern development practices.", "ASP.NET Core Web API", 69.99m, 4.5m, "aspnet-core-webapi", 920, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Complete Node.js and Express.js course with MongoDB and authentication.", 190, 1, true, 2, "Build full-featured backend applications with Node.js, Express.js, MongoDB, and implement secure authentication and authorization.", "Node.js & Express Masterclass", 64.99m, 4.4m, "nodejs-express-masterclass", 1100, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Complete MERN stack course: MongoDB, Express, React, and Node.js.", 300, 1, true, 3, "Master the complete MERN stack by building real-world applications. Learn MongoDB, Express.js, React, and Node.js with modern development practices.", "Full Stack MERN Development", 99.99m, 4.8m, "fullstack-mern-development", 650, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Learn the fundamentals of user interface and user experience design.", 140, 4, true, 1, "Discover the principles of great UI/UX design, learn design thinking processes, and create user-centered designs that convert.", "UI/UX Design Fundamentals", 54.99m, 4.6m, "uiux-design-fundamentals", 980, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Master Figma with advanced techniques, components, and design systems.", 160, 4, true, 2, "Take your Figma skills to the next level with advanced prototyping, component systems, design tokens, and collaborative workflows.", "Advanced Figma for Designers", 74.99m, 4.7m, "advanced-figma-designers", 540, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
