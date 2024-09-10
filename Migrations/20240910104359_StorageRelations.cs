using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineCellar.Migrations
{
    /// <inheritdoc />
    public partial class StorageRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wines_Storages_StorageId",
                table: "Wines");

            migrationBuilder.AlterColumn<int>(
                name: "StorageId",
                table: "Wines",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Wines_Storages_StorageId",
                table: "Wines",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wines_Storages_StorageId",
                table: "Wines");

            migrationBuilder.AlterColumn<int>(
                name: "StorageId",
                table: "Wines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wines_Storages_StorageId",
                table: "Wines",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
