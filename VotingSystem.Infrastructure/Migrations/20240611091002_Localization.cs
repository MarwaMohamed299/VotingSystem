using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Localization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Questions",
                newName: "TextEn");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Polls",
                newName: "TitleEn");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Options",
                newName: "DescriptionEn");

            migrationBuilder.AddColumn<string>(
                name: "TextAr",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleAr",
                table: "Polls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                table: "Options",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 1,
                column: "DescriptionAr",
                value: "");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 2,
                column: "DescriptionAr",
                value: "");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 3,
                column: "DescriptionAr",
                value: "");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 4,
                column: "DescriptionAr",
                value: "");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 5,
                column: "DescriptionAr",
                value: "");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 6,
                column: "DescriptionAr",
                value: "");

            migrationBuilder.UpdateData(
                table: "Polls",
                keyColumn: "PollId",
                keyValue: 1,
                column: "TitleAr",
                value: "");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 1,
                column: "TextAr",
                value: "");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 2,
                column: "TextAr",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextAr",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TitleAr",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                table: "Options");

            migrationBuilder.RenameColumn(
                name: "TextEn",
                table: "Questions",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "Polls",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DescriptionEn",
                table: "Options",
                newName: "Description");
        }
    }
}
