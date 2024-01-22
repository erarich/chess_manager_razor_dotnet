using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class PlayerModelView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerViewModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerViewModel_TournamentViewModel_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "TournamentViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerViewModel_TournamentId",
                table: "PlayerViewModel",
                column: "TournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerViewModel");
        }
    }
}
