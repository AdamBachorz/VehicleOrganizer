using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleOrganizer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTechnicalReviewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastTechnicalReview",
                table: "Vehicles",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NextTechnicalReview",
                table: "Vehicles",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastTechnicalReview",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "NextTechnicalReview",
                table: "Vehicles");
        }
    }
}
