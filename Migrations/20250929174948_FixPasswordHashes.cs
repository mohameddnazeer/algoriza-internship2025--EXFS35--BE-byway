using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byway.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixPasswordHashes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$nZvVSaYzuzeIj.eeyJrjgOu9lmDlrdQfOqly/aBkmL9OgojqXfHtK");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsAdmin", "LastName", "PasswordHash", "PhoneNumber", "ProfileImageBase64", "UpdatedAt" },
                values: new object[] { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "user@byway.com", "John", false, "Doe", "$2a$11$Yy7jamVdZR6YudLy/6.fxeUMI8/.WuVZHA2kzAOtGXzqVoXyQqGJy", null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$8K1p/a0dL2LkqvMA/YdYSuL9/dI52laHymFh17CpOkMob6NjMgcmy");
        }
    }
}
