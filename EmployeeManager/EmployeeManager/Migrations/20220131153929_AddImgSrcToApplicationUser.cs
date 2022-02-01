using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class AddImgSrcToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05326501-45e2-446f-96a3-644fb8749dc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1452b23f-1bf3-4b1d-9f93-6c4ff46974b8");

            migrationBuilder.AddColumn<string>(
                name: "ImgSrc",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "81b90640-5e86-4616-86aa-3485c613179f", "8ef26121-d22c-4c87-897f-efe3a4a35c68", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1bde998f-979f-4c57-8dd4-e05a8784d86e", "188e5cf5-93a7-4ffe-9db9-d412b901500f", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bde998f-979f-4c57-8dd4-e05a8784d86e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81b90640-5e86-4616-86aa-3485c613179f");

            migrationBuilder.DropColumn(
                name: "ImgSrc",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1452b23f-1bf3-4b1d-9f93-6c4ff46974b8", "50c5719b-ee41-49b3-9790-1b03eb515360", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "05326501-45e2-446f-96a3-644fb8749dc6", "bfa27f34-0513-41d6-947c-a563c4751233", "Employee", "EMPLOYEE" });
        }
    }
}
