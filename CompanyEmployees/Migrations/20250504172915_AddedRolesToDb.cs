using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "80abbca8-664d-4b20-b5de-024705497d4a", null, "Manager", "MANAGER" },
                    { "86dba8c0-d178-41e7-938c-ed49778fb52a", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80abbca8-664d-4b20-b5de-024705497d4a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86dba8c0-d178-41e7-938c-ed49778fb52a");
        }
    }
}
