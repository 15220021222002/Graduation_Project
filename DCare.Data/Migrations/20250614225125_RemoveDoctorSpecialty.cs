using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDoctorSpecialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSpecialties");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialties_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "SpecialtyId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialties_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Specialties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DoctorSpecialties",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecialties", x => new { x.DoctorId, x.SpecialtyId });
                    table.ForeignKey(
                        name: "FK_DoctorSpecialties_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialties_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "SpecialtyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialties_SpecialtyId",
                table: "DoctorSpecialties",
                column: "SpecialtyId");
        }
    }
}
