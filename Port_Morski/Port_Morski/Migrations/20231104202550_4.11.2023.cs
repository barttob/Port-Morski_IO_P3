using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Port_Morski.Migrations
{
    /// <inheritdoc />
    public partial class _4112023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Docks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ship_id = table.Column<int>(type: "int", nullable: true),
                    dock_id = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Operations_Docks",
                        column: x => x.dock_id,
                        principalTable: "Docks",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Operations_Ships",
                        column: x => x.ship_id,
                        principalTable: "Ships",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_dock_id",
                table: "Operations",
                column: "dock_id");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ship_id",
                table: "Operations",
                column: "ship_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Docks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
