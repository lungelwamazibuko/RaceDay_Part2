using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDay_Part2.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabae : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblEvent",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEvent", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "TblRegistration",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BibNumber = table.Column<int>(type: "int", nullable: false),
                    RegistrationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRegistration", x => x.RegistrationId);
                });

            migrationBuilder.CreateTable(
                name: "TblResult",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(type: "int", nullable: false),
                    FinishTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    ResultStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblResult", x => x.ResultId);
                });

            migrationBuilder.CreateTable(
                name: "TblRoute",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    RouteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistanceKm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRoute", x => x.RouteId);
                });

            migrationBuilder.CreateTable(
                name: "TblWeather",
                columns: table => new
                {
                    WeatherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    WeatherDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindSpeed = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblWeather", x => x.WeatherId);
                });

            migrationBuilder.CreateTable(
                name: "TblCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumAge = table.Column<int>(type: "int", nullable: true),
                    MaximumAge = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCategory", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_TblCategory_TblEvent_EventId",
                        column: x => x.EventId,
                        principalTable: "TblEvent",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblCategory_TblRoute_RouteId",
                        column: x => x.RouteId,
                        principalTable: "TblRoute",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblCategory_EventId",
                table: "TblCategory",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_TblCategory_RouteId",
                table: "TblCategory",
                column: "RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblCategory");

            migrationBuilder.DropTable(
                name: "TblRegistration");

            migrationBuilder.DropTable(
                name: "TblResult");

            migrationBuilder.DropTable(
                name: "TblWeather");

            migrationBuilder.DropTable(
                name: "TblEvent");

            migrationBuilder.DropTable(
                name: "TblRoute");
        }
    }
}
