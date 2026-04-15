using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SonDoongBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailNguoiDat",
                table: "Bookings",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailNguoiDat",
                table: "Bookings");
        }
    }
}
