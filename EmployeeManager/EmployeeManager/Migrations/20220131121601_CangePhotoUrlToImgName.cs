using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class CangePhotoUrlToImgName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4af9592d-699a-4fea-8ff2-67987ad53657");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5c0ca9e-a5ea-41b5-9f36-46592b4ab2a6");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "AspNetUsers",
                newName: "ImgName");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1452b23f-1bf3-4b1d-9f93-6c4ff46974b8", "50c5719b-ee41-49b3-9790-1b03eb515360", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "05326501-45e2-446f-96a3-644fb8749dc6", "bfa27f34-0513-41d6-947c-a563c4751233", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05326501-45e2-446f-96a3-644fb8749dc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1452b23f-1bf3-4b1d-9f93-6c4ff46974b8");

            migrationBuilder.RenameColumn(
                name: "ImgName",
                table: "AspNetUsers",
                newName: "PhotoUrl");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e5c0ca9e-a5ea-41b5-9f36-46592b4ab2a6", "b79377f2-ab7e-4549-a68c-286dd1165b39", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4af9592d-699a-4fea-8ff2-67987ad53657", "b546c5af-426b-432c-b950-7828b0171807", "Employee", "EMPLOYEE" });
        }
    }
}
