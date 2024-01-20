using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class TournamentOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUserId",
                table: "TournamentViewModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentViewModel_OwnerUserId",
                table: "TournamentViewModel",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentViewModel_AspNetUsers_OwnerUserId",
                table: "TournamentViewModel",
                column: "OwnerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentViewModel_AspNetUsers_OwnerUserId",
                table: "TournamentViewModel");

            migrationBuilder.DropIndex(
                name: "IX_TournamentViewModel_OwnerUserId",
                table: "TournamentViewModel");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "TournamentViewModel");
        }
    }
}
