using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SonDoongBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThemDuLieuMau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "Gia", "HinhAnh", "MoTa", "SoNgay", "TenTour" },
                values: new object[,]
                {
                    { 1, 72000000m, "https://cms.junglebosstours.com/assets/e3d81d8f-ae2c-4dc4-819b-a4320160b7ee?width=1200", "Chinh phục hang động lớn nhất thế giới, vượt Bức tường Việt Nam siêu thực.", 4, "Khám phá Sơn Đoòng 4N3Đ" },
                    { 2, 7600000m, "https://i1-dulich.vnecdn.net/2021/03/15/1-Hang-Nuoc-Nut-JPG.jpg", "Trải nghiệm cắm trại trong hang Én khổng lồ, ngắm hàng vạn chim én chao lượn.", 2, "Thám hiểm hang Én 2N1Đ" },
                    { 3, 9000000m, "https://resource.kinhtedothi.vn/2021/12/25/sonddong1.jpg", "Chiêm ngưỡng vương quốc thạch nhũ tháp nón mọc lên từ mặt nước độc đáo nhất thế giới.", 2, "Trải nghiệm hang Va 2N1Đ" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
