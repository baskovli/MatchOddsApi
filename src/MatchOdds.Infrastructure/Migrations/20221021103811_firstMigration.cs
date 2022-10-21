using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchOdds.Infrastructure.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValue: ""),
                    MatchDate = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValue: ""),
                    MatchTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: ""),
                    TeamA = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    TeamB = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    Sport = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Odds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Specifier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValue: ""),
                    Odd = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Odds_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchDate_TeamA",
                table: "Matches",
                columns: new[] { "MatchDate", "TeamA" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchDate_TeamA_TeamB",
                table: "Matches",
                columns: new[] { "MatchDate", "TeamA", "TeamB" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchDate_TeamB",
                table: "Matches",
                columns: new[] { "MatchDate", "TeamB" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Odds_MatchId",
                table: "Odds",
                column: "MatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odds");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
