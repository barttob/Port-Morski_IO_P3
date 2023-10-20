using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Port_Morski.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Docks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    capacity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    user_role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    login = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    weight = table.Column<int>(type: "int", nullable: true),
                    ship_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cargo_Ships",
                        column: x => x.ship_id,
                        principalTable: "Ships",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ShipSchedule",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ship_id = table.Column<int>(type: "int", nullable: true),
                    dock_id = table.Column<int>(type: "int", nullable: true),
                    arrive_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    flow_out_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipSchedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_ShipSchedule_Docks",
                        column: x => x.dock_id,
                        principalTable: "Docks",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ShipSchedule_Ships",
                        column: x => x.ship_id,
                        principalTable: "Ships",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "EmpSchedule",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    shift_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpSchedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_EmpSchedule_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_ship_id",
                table: "Cargo",
                column: "ship_id");

            migrationBuilder.CreateIndex(
                name: "IX_EmpSchedule_user_id",
                table: "EmpSchedule",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ShipSchedule_dock_id",
                table: "ShipSchedule",
                column: "dock_id");

            migrationBuilder.CreateIndex(
                name: "IX_ShipSchedule_ship_id",
                table: "ShipSchedule",
                column: "ship_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "EmpSchedule");

            migrationBuilder.DropTable(
                name: "ShipSchedule");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Docks");

            migrationBuilder.DropTable(
                name: "Ships");
        }
    }
}
