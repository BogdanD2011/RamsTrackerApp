using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RamsTrackerAPI.Migrations.RamsAuthDb
{
    /// <inheritdoc />
    public partial class Userprofilecontractoradded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contractor",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contractor",
                table: "AspNetUsers");
        }
    }
}
