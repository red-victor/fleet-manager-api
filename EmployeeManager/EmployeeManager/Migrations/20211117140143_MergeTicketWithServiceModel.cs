using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class MergeTicketWithServiceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicesToProcess");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "348df5df-72e2-4214-9f11-df27b99c48bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "368f3e37-982f-4613-834f-50ad981da8fd");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "86cabb62-4569-4654-9bb4-ec667d58a604", "681d8534-f121-4b96-9298-637d71550ae1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a5010fb1-9f1b-4093-bc5a-da9037bc5c25", "d5a85daf-0369-4786-8d22-2c0f05ce0a8f", "Employee", "EMPLOYEE" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TypeId",
                table: "Tickets",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ServiceTypes_TypeId",
                table: "Tickets",
                column: "TypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ServiceTypes_TypeId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TypeId",
                table: "Tickets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86cabb62-4569-4654-9bb4-ec667d58a604");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5010fb1-9f1b-4093-bc5a-da9037bc5c25");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "ServicesToProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesToProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesToProcess_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicesToProcess_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "368f3e37-982f-4613-834f-50ad981da8fd", "574f6220-64bb-4be9-abb2-c325f27e9bbe", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "348df5df-72e2-4214-9f11-df27b99c48bf", "1b93fa84-24da-4a23-a5b1-06e2aa881f46", "Employee", "EMPLOYEE" });

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToProcess_CarId",
                table: "ServicesToProcess",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToProcess_UserId",
                table: "ServicesToProcess",
                column: "UserId");
        }
    }
}
