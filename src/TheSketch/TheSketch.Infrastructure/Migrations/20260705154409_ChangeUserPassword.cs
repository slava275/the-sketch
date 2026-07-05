using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheSketch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$11$mC7p3vT1XGqK5b9zW8YxUeM4fQ6u2jE9rT3vY5wX8zG1aBbCcDdEe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$11$8K4pYj7VfO4L3mQ2wExZu.uHkGb/6xXwTy9eY6W7Z0G3aBbCcDdEe");
        }
    }
}
