using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Jersey.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addStoreRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Stores",
                newName: "District");

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "Description", "District", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Shop No. M2-008, Level G, Kai Tak Mall 2, Kai Tak Sports Park, Kai Tak, Kowloon City", "Kai Tak Sports Park New Shop", "Kowloon City District, Kowloon", "Kai Tak Sports Park", "(852)23218832" },
                    { 2, "Shop 307, 3F, K11 MUSEA, victoria Dockside, 18 Salisbury Road, Tsim Sha Tsui", "K11 Musea shop", "Yau Tsim Mong District, Kowloon", "K11 Musea", "(852)23220228" },
                    { 3, "Shop331, 3/F, Hopewell Mall, No.15 Kennedy Road, Wan Chai", "Hopewell Mall New Shop", "Wan Chai District, Hong Kong Island", "Hopewell Mall", "(852)23601123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Stores",
                newName: "City");
        }
    }
}
