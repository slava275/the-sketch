using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheSketch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "admin@thesketch.local", "$2a$11$8K4pYj7VfO4L3mQ2wExZu.uHkGb/6xXwTy9eY6W7Z0G3aBbCcDdEe", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));
        }
    }
}
