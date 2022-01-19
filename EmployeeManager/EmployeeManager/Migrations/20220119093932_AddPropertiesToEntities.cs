using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class AddPropertiesToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c143832-c3ad-4358-a2ea-280f366a2d5c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6c50fa6-4bee-4987-b9c9-49fee0b5befe");

            migrationBuilder.AddColumn<int>(
                name: "MileageAtSubmit",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc3cd52d-4dad-4db8-b1bb-56cf6fedff43", "ff1eef04-9aec-48fa-81ce-edccad0ac365", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "849cb649-b747-4194-83fd-57975709432d", "3d2c12ae-bbc8-4a09-8854-8159d5170cf0", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "849cb649-b747-4194-83fd-57975709432d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3cd52d-4dad-4db8-b1bb-56cf6fedff43");

            migrationBuilder.DropColumn(
                name: "MileageAtSubmit",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e6c50fa6-4bee-4987-b9c9-49fee0b5befe", "eb7f3c52-b18e-4fc6-8896-96a4fb95f9bf", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8c143832-c3ad-4358-a2ea-280f366a2d5c", "64e0dcbe-de78-4b85-a11b-42b3304a45f2", "Employee", "EMPLOYEE" });
        }
    }
}
