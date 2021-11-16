using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.Migrations
{
    public partial class UpdateCarTableToHaveIdPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarServices_Cars_CarChassisSeries",
                table: "CarServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesToProcess_Cars_CarChassisSeries",
                table: "ServicesToProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Cars_CarChassisSeries",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CarChassisSeries",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_ServicesToProcess_CarChassisSeries",
                table: "ServicesToProcess");

            migrationBuilder.DropIndex(
                name: "IX_CarServices_CarChassisSeries",
                table: "CarServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarChassisSeries",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CarChassisSeries",
                table: "ServicesToProcess");

            migrationBuilder.DropColumn(
                name: "CarChassisSeries",
                table: "CarServices");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "ServicesToProcess",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "CarServices",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChassisSeries",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CarId",
                table: "Tickets",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToProcess_CarId",
                table: "ServicesToProcess",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarServices_CarId",
                table: "CarServices",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarServices_Cars_CarId",
                table: "CarServices",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesToProcess_Cars_CarId",
                table: "ServicesToProcess",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Cars_CarId",
                table: "Tickets",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarServices_Cars_CarId",
                table: "CarServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesToProcess_Cars_CarId",
                table: "ServicesToProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Cars_CarId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CarId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_ServicesToProcess_CarId",
                table: "ServicesToProcess");

            migrationBuilder.DropIndex(
                name: "IX_CarServices_CarId",
                table: "CarServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "ServicesToProcess");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "CarServices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "CarChassisSeries",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarChassisSeries",
                table: "ServicesToProcess",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarChassisSeries",
                table: "CarServices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChassisSeries",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "ChassisSeries");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CarChassisSeries",
                table: "Tickets",
                column: "CarChassisSeries");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToProcess_CarChassisSeries",
                table: "ServicesToProcess",
                column: "CarChassisSeries");

            migrationBuilder.CreateIndex(
                name: "IX_CarServices_CarChassisSeries",
                table: "CarServices",
                column: "CarChassisSeries");

            migrationBuilder.AddForeignKey(
                name: "FK_CarServices_Cars_CarChassisSeries",
                table: "CarServices",
                column: "CarChassisSeries",
                principalTable: "Cars",
                principalColumn: "ChassisSeries",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesToProcess_Cars_CarChassisSeries",
                table: "ServicesToProcess",
                column: "CarChassisSeries",
                principalTable: "Cars",
                principalColumn: "ChassisSeries",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Cars_CarChassisSeries",
                table: "Tickets",
                column: "CarChassisSeries",
                principalTable: "Cars",
                principalColumn: "ChassisSeries",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
