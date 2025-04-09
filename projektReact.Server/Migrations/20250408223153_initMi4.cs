using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projektReact.Server.Migrations
{
    /// <inheritdoc />
    public partial class initMi4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "sdadsaookl",
                column: "Password",
                value: "AQAAAAIAAYagAAAAEF907Qk18SWRe2yk2KD8aCqeWbhwj26sjSPIauUXK+Evz407llzQk+6j6XMeHJHDBA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "sdadsaookl",
                column: "Password",
                value: "AQAAAAIAAYagAAAAEJfh+f4Yx6xs2AmXxQliZLoqg/6aziAeH4t8LBs/wa0s/y6l/r3ZRzbqh5UObewQyg==");
        }
    }
}
