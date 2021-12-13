using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class RenameCarServiceToCarHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarServices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bb224e3-2479-4ab6-817a-8c9ee594562f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a53d92ff-c532-4a34-b0e1-1a784c1201a6");

            migrationBuilder.CreateTable(
                name: "CarHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    ServiceType = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "varchar(max)", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MileageAtExecution = table.Column<int>(type: "int", nullable: false),
                    RenewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarHistory_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a14dce5d-03c4-48a4-aa09-7e945369d253", "82b15d90-a289-4764-9222-6c722cb96368", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "56cff68d-3cde-4767-9d76-cad53cf51ad6", "d6268819-5726-4d0b-a8a6-a676fe0df488", "Employee", "EMPLOYEE" });

            migrationBuilder.CreateIndex(
                name: "IX_CarHistory_CarId",
                table: "CarHistory",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarHistory");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56cff68d-3cde-4767-9d76-cad53cf51ad6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a14dce5d-03c4-48a4-aa09-7e945369d253");

            migrationBuilder.CreateTable(
                name: "CarServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Details = table.Column<string>(type: "varchar(max)", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false),
                    MileageAtExecution = table.Column<int>(type: "int", nullable: false),
                    RenewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarServices_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a53d92ff-c532-4a34-b0e1-1a784c1201a6", "0986c46c-3d7c-480f-82ac-b12a5fabbd06", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3bb224e3-2479-4ab6-817a-8c9ee594562f", "ab808dbd-12c9-4882-8e68-095bc7c2f039", "Employee", "EMPLOYEE" });

            migrationBuilder.CreateIndex(
                name: "IX_CarServices_CarId",
                table: "CarServices",
                column: "CarId");
        }
    }
}
