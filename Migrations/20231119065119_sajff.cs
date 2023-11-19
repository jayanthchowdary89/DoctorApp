using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorApp.Migrations
{
    public partial class sajff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_approved",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "Appointment_Fee",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DId",
                table: "Appointments",
                column: "DId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PId",
                table: "Appointments",
                column: "PId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DId",
                table: "Appointments",
                column: "DId",
                principalTable: "Doctors",
                principalColumn: "DId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PId",
                table: "Appointments",
                column: "PId",
                principalTable: "Patients",
                principalColumn: "PId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PId",
                table: "Appointments");

            migrationBuilder.AlterColumn<decimal>(
                name: "Appointment_Fee",
                table: "Appointments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Is_approved",
                table: "Appointments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
