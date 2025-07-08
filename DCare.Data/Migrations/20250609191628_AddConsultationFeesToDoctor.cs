using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConsultationFeesToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "SecretaryPhoneNumber",
                table: "Doctors",
                newName: "QualificationImgUrl");

            migrationBuilder.RenameColumn(
                name: "QualificationsFile",
                table: "Doctors",
                newName: "LicenseImgUrl");

            migrationBuilder.RenameColumn(
                name: "LicenseFile",
                table: "Doctors",
                newName: "DoctorImgUrl");

            migrationBuilder.AddColumn<decimal>(
                name: "fees",
                table: "Doctors",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fees",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "QualificationImgUrl",
                table: "Doctors",
                newName: "SecretaryPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "LicenseImgUrl",
                table: "Doctors",
                newName: "QualificationsFile");

            migrationBuilder.RenameColumn(
                name: "DoctorImgUrl",
                table: "Doctors",
                newName: "LicenseFile");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
