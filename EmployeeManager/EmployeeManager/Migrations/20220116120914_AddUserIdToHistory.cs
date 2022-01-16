using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class AddUserIdToHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "131ab473-62ea-47e3-a66a-7e690184efaf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ad59023-7ac8-4c67-b418-4ba0d6821970");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CarHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e6c50fa6-4bee-4987-b9c9-49fee0b5befe", "eb7f3c52-b18e-4fc6-8896-96a4fb95f9bf", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8c143832-c3ad-4358-a2ea-280f366a2d5c", "64e0dcbe-de78-4b85-a11b-42b3304a45f2", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c143832-c3ad-4358-a2ea-280f366a2d5c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6c50fa6-4bee-4987-b9c9-49fee0b5befe");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CarHistory");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ad59023-7ac8-4c67-b418-4ba0d6821970", "498fcb55-1fae-4fd1-a1dc-cdd88aed8846", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "131ab473-62ea-47e3-a66a-7e690184efaf", "14a2453c-bb52-4192-9733-eec573d581c9", "Employee", "EMPLOYEE" });
        }
    }
}
