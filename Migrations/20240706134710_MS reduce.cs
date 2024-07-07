using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RamsTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class MSreduce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MS_Contractor_ContractorId",
                table: "MS");

            migrationBuilder.DropForeignKey(
                name: "FK_MS_RA_RaId",
                table: "MS");

            migrationBuilder.DropIndex(
                name: "IX_MS_ContractorId",
                table: "MS");

            migrationBuilder.DropIndex(
                name: "IX_MS_RaId",
                table: "MS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MS_ContractorId",
                table: "MS",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_MS_RaId",
                table: "MS",
                column: "RaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MS_Contractor_ContractorId",
                table: "MS",
                column: "ContractorId",
                principalTable: "Contractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MS_RA_RaId",
                table: "MS",
                column: "RaId",
                principalTable: "RA",
                principalColumn: "Id");
        }
    }
}
