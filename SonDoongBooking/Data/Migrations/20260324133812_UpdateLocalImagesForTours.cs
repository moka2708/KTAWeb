using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SonDoongBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLocalImagesForTours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                column: "HinhAnh",
                value: "/images/anhtour1.webp");

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2,
                column: "HinhAnh",
                value: "/images/anhtour2.avif");

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3,
                column: "HinhAnh",
                value: "/images/anhtour3.webp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                column: "HinhAnh",
                value: "https://cms.junglebosstours.com/assets/e3d81d8f-ae2c-4dc4-819b-a4320160b7ee?width=1200");

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2,
                column: "HinhAnh",
                value: "https://i1-dulich.vnecdn.net/2021/03/15/1-Hang-Nuoc-Nut-JPG.jpg");

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3,
                column: "HinhAnh",
                value: "https://resource.kinhtedothi.vn/2021/12/25/sonddong1.jpg");
        }
    }
}
