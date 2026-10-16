using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDay_Part2.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTblCategorytoNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCategory_TblRoute_RouteId",
                table: "TblCategory");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "TblCategory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "TblCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_TblCategory_TblRoute_RouteId",
                table: "TblCategory",
                column: "RouteId",
                principalTable: "TblRoute",
                principalColumn: "RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCategory_TblRoute_RouteId",
                table: "TblCategory");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "TblCategory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "TblCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TblCategory_TblRoute_RouteId",
                table: "TblCategory",
                column: "RouteId",
                principalTable: "TblRoute",
                principalColumn: "RouteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
