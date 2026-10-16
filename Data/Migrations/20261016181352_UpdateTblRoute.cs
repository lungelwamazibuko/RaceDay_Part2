using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDay_Part2.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTblRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TblRoute");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "TblRoute");

            migrationBuilder.AlterColumn<string>(
                name: "RouteName",
                table: "TblRoute",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DistanceKm",
                table: "TblRoute",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RouteName",
                table: "TblRoute",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<decimal>(
                name: "DistanceKm",
                table: "TblRoute",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TblRoute",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "TblRoute",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
