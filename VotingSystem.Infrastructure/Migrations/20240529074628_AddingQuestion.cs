using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VotingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Polls_PollId",
                table: "Options");

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
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "PollId",
                table: "Options",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_PollId",
                table: "Options",
                newName: "IX_Options_QuestionId");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PollId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "PollId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 4,
                column: "QuestionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 5,
                column: "Description",
                value: "TypeScript");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 6,
                column: "Description",
                value: "Dart");

            migrationBuilder.UpdateData(
                table: "Polls",
                keyColumn: "PollId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "PollId", "Text" },
                values: new object[,]
                {
                    { 1, 1, "What is your favorite backend language?" },
                    { 2, 1, "What is your favorite frontend language?" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_PollId",
                table: "Questions",
                column: "PollId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Options",
                newName: "PollId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                newName: "IX_Options_PollId");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 4,
                column: "PollId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 5,
                column: "Description",
                value: "Visual Studio");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 6,
                column: "Description",
                value: "IntelliJ IDEA");

            migrationBuilder.UpdateData(
                table: "Polls",
                keyColumn: "PollId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 6, 7, 12, 34, 31, 854, DateTimeKind.Local).AddTicks(4359), new DateTime(2024, 5, 28, 12, 34, 31, 854, DateTimeKind.Local).AddTicks(4347) });

            migrationBuilder.InsertData(
                table: "Polls",
                columns: new[] { "PollId", "EndDate", "StartDate", "Title" },
                values: new object[] { 2, new DateTime(2024, 6, 7, 12, 34, 31, 854, DateTimeKind.Local).AddTicks(4368), new DateTime(2024, 5, 28, 12, 34, 31, 854, DateTimeKind.Local).AddTicks(4367), "Best IDE" });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionId", "Description", "PollId" },
                values: new object[,]
                {
                    { 7, "PyCharm", 2 },
                    { 8, "VS Code", 2 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Polls_PollId",
                table: "Options",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "PollId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
