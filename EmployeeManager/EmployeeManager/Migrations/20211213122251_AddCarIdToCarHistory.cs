using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class AddCarIdToCarHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarHistory_Cars_CarId",
                table: "CarHistory");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56cff68d-3cde-4767-9d76-cad53cf51ad6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a14dce5d-03c4-48a4-aa09-7e945369d253");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "CarHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42f3a5e3-3388-4430-9a6b-8ce1057188d3", "71f89499-d5ec-47e1-a363-3eb80570dada", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "55a26f42-ddbe-4621-8915-72cd6a415b6e", "472a99fe-26be-4b45-89b2-1acef3137a06", "Employee", "EMPLOYEE" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarHistory_Cars_CarId",
                table: "CarHistory",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarHistory_Cars_CarId",
                table: "CarHistory");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42f3a5e3-3388-4430-9a6b-8ce1057188d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55a26f42-ddbe-4621-8915-72cd6a415b6e");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "CarHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a14dce5d-03c4-48a4-aa09-7e945369d253", "82b15d90-a289-4764-9222-6c722cb96368", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "56cff68d-3cde-4767-9d76-cad53cf51ad6", "d6268819-5726-4d0b-a8a6-a676fe0df488", "Employee", "EMPLOYEE" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarHistory_Cars_CarId",
                table: "CarHistory",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
