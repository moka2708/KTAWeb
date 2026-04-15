using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SonDoongBooking.Models;

namespace SonDoongBooking.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tour> Tours { get; set; } 
    public DbSet<Booking> Bookings { get; set; }

    // BƯỚC NÀY ĐỂ ĐỔ DỮ LIỆU MẪU VÀO DATABASE
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Tour>().HasData(
            new Tour { 
                Id = 1, 
                TenTour = "Khám phá Sơn Đoòng 4N3Đ", 
                MoTa = "Chinh phục hang động lớn nhất thế giới, vượt Bức tường Việt Nam siêu thực.", 
                Gia = 72000000, 
                SoNgay = 4, 
                HinhAnh = "/images/anhtour1.webp" 
            },
            new Tour { 
                Id = 2, 
                TenTour = "Thám hiểm hang Én 2N1Đ", 
                MoTa = "Trải nghiệm cắm trại trong hang Én khổng lồ, ngắm hàng vạn chim én chao lượn.", 
                Gia = 7600000, 
                SoNgay = 2, 
                HinhAnh = "/images/anhtour2.avif" 
            },
            new Tour { 
                Id = 3, 
                TenTour = "Trải nghiệm hang Va 2N1Đ", 
                MoTa = "Chiêm ngưỡng vương quốc thạch nhũ tháp nón mọc lên từ mặt nước độc đáo nhất thế giới.", 
                Gia = 9000000, 
                SoNgay = 2, 
                HinhAnh = "/images/anhtour3.webp" 
            }
        );
    }
}