using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SonDoongBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThemTrangThaiDonHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "Bookings",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "Bookings");
        }
    }
}
