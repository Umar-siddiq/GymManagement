using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTestUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "City", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Full_Name", "Gender", "Height", "LockoutEnabled", "LockoutEnd", "Membership", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName", "Weight" },
                values: new object[] { "14a375b4-db7b-466f-9a83-f6784a00f918", 0, 21, "Khaitan", "1cc249d1-ae1e-4b76-b6ca-116bb00538b8", "GymUser", null, false, "Test Name", "M", 170, false, null, false, null, null, null, null, false, "33dd35bb-a089-492f-9330-6ea61836973e", false, "FullBody", null, 55 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14a375b4-db7b-466f-9a83-f6784a00f918");
        }
    }
}
