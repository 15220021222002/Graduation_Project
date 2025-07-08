using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAboutDoctorProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutDoctor",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutDoctor",
                table: "Doctors");
        }
    }
}
