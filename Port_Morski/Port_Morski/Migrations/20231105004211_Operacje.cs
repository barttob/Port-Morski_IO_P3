using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Port_Morski.Migrations
{
    /// <inheritdoc />
    public partial class Operacje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Docks",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Ships",
                table: "Operations");

            migrationBuilder.AddColumn<bool>(
                name: "approved",
                table: "Operations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Docks",
                table: "Operations",
                column: "ship_id",
                principalTable: "Docks",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Ships",
                table: "Operations",
                column: "dock_id",
                principalTable: "Ships",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Docks",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Ships",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "approved",
                table: "Operations");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Docks",
                table: "Operations",
                column: "dock_id",
                principalTable: "Docks",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Ships",
                table: "Operations",
                column: "ship_id",
                principalTable: "Ships",
                principalColumn: "id");
        }
    }
}
