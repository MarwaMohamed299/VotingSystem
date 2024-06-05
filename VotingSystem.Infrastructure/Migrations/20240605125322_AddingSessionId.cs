using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingSessionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Votes",
                newName: "SessionIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SessionIdentifier",
                table: "Votes",
                newName: "SessionId");
        }
    }
}
