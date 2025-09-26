using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperTix.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Game_Game_GameID1",
                        column: x => x.GameID1,
                        principalTable: "Game",
                        principalColumn: "GameID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameID1",
                table: "Game",
                column: "GameID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
