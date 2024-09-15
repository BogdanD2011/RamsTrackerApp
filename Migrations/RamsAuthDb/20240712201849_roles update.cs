using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RamsTrackerAPI.Migrations.RamsAuthDb
{
    /// <inheritdoc />
    public partial class rolesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4F60EAE4-0AE1-4C9E-BA31-91C5F5232CC7",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Subcontractor", "SUBCONTRACTOR" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B99383C7-F325-467F-9CC4-4E28BBBFF890",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Contractor", "CONTRACTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3DF7535C-E623-4856-8892-81FA949F591F", "3DF7535C-E623-4856-8892-81FA949F591F", "GeneralAdmin", "GENERALADMIN" },
                    { "6AD221F1-7524-4624-9513-208FA12F28DC", "6AD221F1-7524-4624-9513-208FA12F28DC", "GroupAdmin", "GROUPADMIN" },
                    { "70A9E9A7-A94F-4B65-A942-BF946E11D699", "70A9E9A7-A94F-4B65-A942-BF946E11D699", "H&S_Representant", "H&S_REPRESENTANT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3DF7535C-E623-4856-8892-81FA949F591F");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6AD221F1-7524-4624-9513-208FA12F28DC");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70A9E9A7-A94F-4B65-A942-BF946E11D699");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4F60EAE4-0AE1-4C9E-BA31-91C5F5232CC7",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Reader", "READER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B99383C7-F325-467F-9CC4-4E28BBBFF890",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Writer", "WRITER" });
        }
    }
}
