using Microsoft.EntityFrameworkCore.Migrations;

namespace HighscoreApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PName = table.Column<string>(maxLength: 3, nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "PName", "Score" },
                values: new object[,]
                {
                    { 1, "BRU", 17383 },
                    { 2, "BRO", 1738 },
                    { 3, "BRA", 1783 },
                    { 4, "BRE", 1383 },
                    { 5, "BRI", 383 },
                    { 6, "COK", 7383 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
