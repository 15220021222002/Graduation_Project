using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProfessionalTitleToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfessionalTitle",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfessionalTitle",
                table: "Doctors");
        }
    }
}
