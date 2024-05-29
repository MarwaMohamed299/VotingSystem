using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EdittingVoter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Voters");

            migrationBuilder.AddColumn<bool>(
                name: "hasSubmitted",
                table: "Voters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Polls",
                keyColumn: "PollId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 6, 7, 12, 34, 31, 854, DateTimeKind.Local).AddTicks(4359), new DateTime(2024, 5, 28, 12, 34, 31, 854, DateTimeKind.Local).AddTicks(4347) });

            migrationBuilder.UpdateData(
                table: "Polls",
                keyColumn: "PollId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 6, 7, 12, 34, 31, 854, DateTimeKind.Local).AddTicks(4368), new DateTime(2024, 5, 28, 12, 34, 31, 854, DateTimeKind.Local).AddTicks(4367) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasSubmitted",
                table: "Voters");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Voters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Voters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Polls",
                keyColumn: "PollId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 6, 7, 9, 51, 41, 202, DateTimeKind.Local).AddTicks(2436), new DateTime(2024, 5, 28, 9, 51, 41, 202, DateTimeKind.Local).AddTicks(2421) });

            migrationBuilder.UpdateData(
                table: "Polls",
                keyColumn: "PollId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 6, 7, 9, 51, 41, 202, DateTimeKind.Local).AddTicks(2446), new DateTime(2024, 5, 28, 9, 51, 41, 202, DateTimeKind.Local).AddTicks(2445) });
        }
    }
}
