using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineCellar.Migrations
{
    /// <inheritdoc />
    public partial class WineRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wines",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(name: "IX_Wines_Name", table: "Wines", column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Wines_Quantity",
                table: "Wines",
                column: "Quantity"
            );

            migrationBuilder.CreateIndex(name: "IX_Wines_Type", table: "Wines", column: "Type");

            migrationBuilder.CreateIndex(name: "IX_Wines_UserId", table: "Wines", column: "UserId");

            migrationBuilder.CreateIndex(name: "IX_Wines_Year", table: "Wines", column: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Wines");
        }
    }
}
