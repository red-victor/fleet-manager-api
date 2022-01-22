using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class AddUnconfirmedEmailToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "849cb649-b747-4194-83fd-57975709432d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3cd52d-4dad-4db8-b1bb-56cf6fedff43");

            migrationBuilder.AddColumn<string>(
                name: "UnConfirmedEmail",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e5c0ca9e-a5ea-41b5-9f36-46592b4ab2a6", "b79377f2-ab7e-4549-a68c-286dd1165b39", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4af9592d-699a-4fea-8ff2-67987ad53657", "b546c5af-426b-432c-b950-7828b0171807", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4af9592d-699a-4fea-8ff2-67987ad53657");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5c0ca9e-a5ea-41b5-9f36-46592b4ab2a6");

            migrationBuilder.DropColumn(
                name: "UnConfirmedEmail",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc3cd52d-4dad-4db8-b1bb-56cf6fedff43", "ff1eef04-9aec-48fa-81ce-edccad0ac365", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "849cb649-b747-4194-83fd-57975709432d", "3d2c12ae-bbc8-4a09-8854-8159d5170cf0", "Employee", "EMPLOYEE" });
        }
    }
}
