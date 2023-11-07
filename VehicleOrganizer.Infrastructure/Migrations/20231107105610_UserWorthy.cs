using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleOrganizer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserWorthy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceCode",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceCode",
                table: "Users");
        }
    }
}
