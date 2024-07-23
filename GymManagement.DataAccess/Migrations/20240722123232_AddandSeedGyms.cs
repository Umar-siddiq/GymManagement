using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddandSeedGyms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gyms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DumbBells = table.Column<int>(type: "int", nullable: false),
                    Treadmills = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Gyms",
                columns: new[] { "Id", "DumbBells", "Location", "Treadmills" },
                values: new object[,]
                {
                    { 1, 15, "Street 12 Abraq Khaitan", 3 },
                    { 2, 20, "Street 23 Salmiya", 6 },
                    { 3, 25, "Street 27 Kuwait City", 5 },
                    { 4, 34, "Street 31 Jabriya", 7 },
                    { 5, 17, "Street 35 Salwa", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gyms");
        }
    }
}
