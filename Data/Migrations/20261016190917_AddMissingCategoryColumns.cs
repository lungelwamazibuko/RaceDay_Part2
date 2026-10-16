using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDay_Part2.Data.Migrations
{
    public partial class AddMissingCategoryColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "TblCategory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DistanceKm",
                table: "TblCategory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblCategory_RouteId",
                table: "TblCategory",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblCategory_TblRoute_RouteId",
                table: "TblCategory",
                column: "RouteId",
                principalTable: "TblRoute",
                principalColumn: "RouteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCategory_TblRoute_RouteId",
                table: "TblCategory");

            migrationBuilder.DropIndex(
                name: "IX_TblCategory_RouteId",
                table: "TblCategory");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "TblCategory");

            migrationBuilder.DropColumn(
                name: "DistanceKm",
                table: "TblCategory");
        }
    }
}
