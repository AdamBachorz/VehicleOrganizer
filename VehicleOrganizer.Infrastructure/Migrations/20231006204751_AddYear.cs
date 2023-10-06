using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleOrganizer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearOfProduction",
                table: "Vehicles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderDate",
                table: "OperationalActivities",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearOfProduction",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ReminderDate",
                table: "OperationalActivities");
        }
    }
}
