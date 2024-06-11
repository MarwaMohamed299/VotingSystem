using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 1,
                column: "DescriptionAr",
                value: "سي شارب");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 2,
                column: "DescriptionAr",
                value: "جافا");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 3,
                column: "DescriptionAr",
                value: "بايثون");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 4,
                column: "DescriptionAr",
                value: "جافا سكريبت");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 5,
                column: "DescriptionAr",
                value: "تايب سكريبت");

            migrationBuilder.UpdateData(
                table: "Options",
                keyColumn: "OptionId",
                keyValue: 6,
                column: "DescriptionAr",
                value: "دارت");

            migrationBuilder.UpdateData(
                table: "Polls",
                keyColumn: "PollId",
                keyValue: 1,
                column: "TitleAr",
                value: "لغة البرمجة المفضلة");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 1,
                column: "TextAr",
                value: "ما هي لغة الخلفية المفضلة لديك؟");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 2,
                column: "TextAr",
                value: "ما هي لغة الواجهة الأمامية المفضلة لديك؟");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
