using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheSketch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewCred : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "admin@admin", "$2a$11$TFHodhhcTrqDwycX/B53EekseJJm8silFkIigyYYD6MylgZFbszg2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "admin@thesketch.local", "$2a$11$mC7p3vT1XGqK5b9zW8YxUeM4fQ6u2jE9rT3vY5wX8zG1aBbCcDdEe" });
        }
    }
}
