using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SonDoongBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaoBangTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenTour = table.Column<string>(type: "TEXT", nullable: false),
                    MoTa = table.Column<string>(type: "TEXT", nullable: true),
                    Gia = table.Column<decimal>(type: "TEXT", nullable: false),
                    SoNgay = table.Column<int>(type: "INTEGER", nullable: false),
                    HinhAnh = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tours");
        }
    }
}
