using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byway.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePasswordHashes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$0OLx8aV100UT8jNFaqPO6e58R9ha5Xsuk34pYP0t9vF5vMY/aShPC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$88JM/LL2Q/5O6ekyUNTf0.2z7XRxKMMPQkuN0VDrQ501Yga.fozZW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$nZvVSaYzuzeIj.eeyJrjgOu9lmDlrdQfOqly/aBkmL9OgojqXfHtK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$Yy7jamVdZR6YudLy/6.fxeUMI8/.WuVZHA2kzAOtGXzqVoXyQqGJy");
        }
    }
}
