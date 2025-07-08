using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleToAllUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Admins");
        }
    }
}
