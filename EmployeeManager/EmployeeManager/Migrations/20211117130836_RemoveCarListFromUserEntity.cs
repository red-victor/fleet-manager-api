using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class RemoveCarListFromUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19886906-d501-4825-893d-6ea520ff6738");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7af4a232-74db-4778-a3eb-afa3e59f572a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "368f3e37-982f-4613-834f-50ad981da8fd", "574f6220-64bb-4be9-abb2-c325f27e9bbe", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "348df5df-72e2-4214-9f11-df27b99c48bf", "1b93fa84-24da-4a23-a5b1-06e2aa881f46", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "348df5df-72e2-4214-9f11-df27b99c48bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "368f3e37-982f-4613-834f-50ad981da8fd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19886906-d501-4825-893d-6ea520ff6738", "754f3c15-eef0-4493-8dfc-d36eb0dcf4cd", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7af4a232-74db-4778-a3eb-afa3e59f572a", "3227b2a5-9546-4241-9559-481c4a0afb1a", "Employee", "EMPLOYEE" });
        }
    }
}
