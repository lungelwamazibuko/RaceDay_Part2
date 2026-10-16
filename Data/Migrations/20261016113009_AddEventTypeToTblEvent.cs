using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDay_Part2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEventTypeToTblEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "TblEvent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "TblEvent");
        }
    }
}
