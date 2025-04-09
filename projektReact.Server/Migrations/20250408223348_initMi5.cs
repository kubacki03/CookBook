using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projektReact.Server.Migrations
{
    /// <inheritdoc />
    public partial class initMi5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "sdadsaookl",
                column: "Password",
                value: "AQAAAAIAAYagAAAAEFacXzmHLjAGh4bGa5X2lO32T424kyKeGpsAVvSYAYr529ULQQ7tUrVJ9E/bFTR7mA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "sdadsaookl",
                column: "Password",
                value: "AQAAAAIAAYagAAAAEF907Qk18SWRe2yk2KD8aCqeWbhwj26sjSPIauUXK+Evz407llzQk+6j6XMeHJHDBA==");
        }
    }
}
