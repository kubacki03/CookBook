using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projektReact.Server.Migrations
{
    /// <inheritdoc />
    public partial class initMi3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { "sdadsaookl", "AQAAAAIAAYagAAAAEJfh+f4Yx6xs2AmXxQliZLoqg/6aziAeH4t8LBs/wa0s/y6l/r3ZRzbqh5UObewQyg==", "admin@wp.pl" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "sdadsaookl");
        }
    }
}
