using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperTix.Migrations
{
    /// <inheritdoc />
    public partial class FixedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Game_GameID1",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_GameID1",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GameID1",
                table: "Game");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_CategoryID",
                table: "Game",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Category_CategoryID",
                table: "Game",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Category_CategoryID",
                table: "Game");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Game_CategoryID",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "GameID1",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameID1",
                table: "Game",
                column: "GameID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Game_GameID1",
                table: "Game",
                column: "GameID1",
                principalTable: "Game",
                principalColumn: "GameID");
        }
    }
}
