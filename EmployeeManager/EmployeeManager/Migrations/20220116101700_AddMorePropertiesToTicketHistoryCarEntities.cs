using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class AddMorePropertiesToTicketHistoryCarEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "414da97e-0e90-4ff8-bd0d-a45dfd0ac951");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81b74f4c-6372-4e36-8f77-e0091a969101");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Tickets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Cars",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "CarHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "CarHistory",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "CarHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "CarHistory",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ad59023-7ac8-4c67-b418-4ba0d6821970", "498fcb55-1fae-4fd1-a1dc-cdd88aed8846", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "131ab473-62ea-47e3-a66a-7e690184efaf", "14a2453c-bb52-4192-9733-eec573d581c9", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "131ab473-62ea-47e3-a66a-7e690184efaf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ad59023-7ac8-4c67-b418-4ba0d6821970");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "CarHistory");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "CarHistory");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "CarHistory");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "CarHistory");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "414da97e-0e90-4ff8-bd0d-a45dfd0ac951", "f6e1112f-e992-49d8-ad10-a7136172487f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "81b74f4c-6372-4e36-8f77-e0091a969101", "5d1d44a4-263d-4baf-abb6-dc0d293ee5fb", "Employee", "EMPLOYEE" });
        }
    }
}
