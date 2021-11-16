using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class AddAdminAndEmployeeRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "73454cb5-28ed-4dc6-9e51-190d1a9b6b45", "1b2ef298-7be2-4c3c-8351-8cf8ac785d72", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb746e24-59ff-4375-b82c-9e4b12a443d5", "71110913-2a95-4da7-ad37-51e0aa4aed8a", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73454cb5-28ed-4dc6-9e51-190d1a9b6b45");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb746e24-59ff-4375-b82c-9e4b12a443d5");
        }
    }
}
