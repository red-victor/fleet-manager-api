using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class ChangeCharToVarcharInCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e570d8a-4879-4e80-9836-54a9c59d289b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8e041d3-2d5b-4f3a-9d48-a56a40b92b6e");

            migrationBuilder.AlterColumn<string>(
                name: "LicencePlate",
                table: "Cars",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(10)");

            migrationBuilder.AlterColumn<string>(
                name: "ChassisSeries",
                table: "Cars",
                type: "varchar(17)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(17)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d67c4c5-e850-4534-91ef-a96f0918b61a", "102bdc29-0d1b-4138-8747-ff1e15dd5d97", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b0648a41-c517-47e9-8759-0804e7b2e0d2", "058fb9ea-3a7d-4edb-9f57-3562b22ca21c", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d67c4c5-e850-4534-91ef-a96f0918b61a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0648a41-c517-47e9-8759-0804e7b2e0d2");

            migrationBuilder.AlterColumn<string>(
                name: "LicencePlate",
                table: "Cars",
                type: "char(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "ChassisSeries",
                table: "Cars",
                type: "char(17)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(17)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1e570d8a-4879-4e80-9836-54a9c59d289b", "dadec0cb-126b-46ab-8e06-8e691c39387a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e8e041d3-2d5b-4f3a-9d48-a56a40b92b6e", "b064bb26-5324-4bf5-a1a0-a8fc27573939", "Employee", "EMPLOYEE" });
        }
    }
}
