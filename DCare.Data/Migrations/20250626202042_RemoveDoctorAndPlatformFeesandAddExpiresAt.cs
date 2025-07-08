using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDoctorAndPlatformFeesandAddExpiresAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorFee",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PlatformFee",
                table: "Payments");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "Appointments");

            migrationBuilder.AddColumn<decimal>(
                name: "DoctorFee",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PlatformFee",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
