using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class AddIdsToNestedObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42f3a5e3-3388-4430-9a6b-8ce1057188d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55a26f42-ddbe-4621-8915-72cd6a415b6e");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff47d1a2-4c07-422d-9ee1-e95357837eb1", "0f1d2bd4-d62c-499f-800c-8c2c7e4164a8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "692c33ee-bb30-47a2-9a36-7a44085590a6", "e412d8b0-1b34-488f-9cb3-9c20a595aaf4", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "692c33ee-bb30-47a2-9a36-7a44085590a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff47d1a2-4c07-422d-9ee1-e95357837eb1");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tickets",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42f3a5e3-3388-4430-9a6b-8ce1057188d3", "71f89499-d5ec-47e1-a363-3eb80570dada", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "55a26f42-ddbe-4621-8915-72cd6a415b6e", "472a99fe-26be-4b45-89b2-1acef3137a06", "Employee", "EMPLOYEE" });
        }
    }
}
