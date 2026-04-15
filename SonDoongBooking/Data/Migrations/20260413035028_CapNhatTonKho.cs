using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SonDoongBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class CapNhatTonKho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoChoConTrong",
                table: "Tours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                column: "SoChoConTrong",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2,
                column: "SoChoConTrong",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3,
                column: "SoChoConTrong",
                value: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoChoConTrong",
                table: "Tours");
        }
    }
}
