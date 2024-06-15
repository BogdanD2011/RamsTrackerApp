using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RamsTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    revision = table.Column<int>(type: "int", nullable: false),
                    DownloadUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcontractor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcontractor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MS_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    revision = table.Column<int>(type: "int", nullable: false),
                    SubcontractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RAid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_RA_RAid",
                        column: x => x.RAid,
                        principalTable: "RA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Subcontractor_SubcontractorId",
                        column: x => x.SubcontractorId,
                        principalTable: "Subcontractor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MS_RAid",
                table: "MS",
                column: "RAid");

            migrationBuilder.CreateIndex(
                name: "IX_MS_SubcontractorId",
                table: "MS",
                column: "SubcontractorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS");

            migrationBuilder.DropTable(
                name: "RA");

            migrationBuilder.DropTable(
                name: "Subcontractor");
        }
    }
}
