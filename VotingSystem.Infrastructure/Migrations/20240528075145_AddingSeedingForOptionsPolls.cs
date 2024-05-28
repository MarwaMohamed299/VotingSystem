using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VotingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingSeedingForOptionsPolls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Polls",
                columns: new[] { "PollId", "EndDate", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 7, 9, 51, 41, 202, DateTimeKind.Local).AddTicks(2436), new DateTime(2024, 5, 28, 9, 51, 41, 202, DateTimeKind.Local).AddTicks(2421), "Favorite Programming Language" },
                    { 2, new DateTime(2024, 6, 7, 9, 51, 41, 202, DateTimeKind.Local).AddTicks(2446), new DateTime(2024, 5, 28, 9, 51, 41, 202, DateTimeKind.Local).AddTicks(2445), "Best IDE" }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionId", "Description", "PollId" },
                values: new object[,]
                {
                    { 1, "C#", 1 },
                    { 2, "Java", 1 },
                    { 3, "Python", 1 },
                    { 4, "JavaScript", 1 },
                    { 5, "Visual Studio", 2 },
                    { 6, "IntelliJ IDEA", 2 },
                    { 7, "PyCharm", 2 },
                    { 8, "VS Code", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Polls",
                keyColumn: "PollId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Polls",
                keyColumn: "PollId",
                keyValue: 2);
        }
    }
}
