using System;
using System.ComponentModel.DataAnnotations;

namespace SonDoongBooking.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        // Lưu xem khách đặt Tour nào (Móc nối với bảng Tour)
        public int TourId { get; set; }
        public string? TrangThai { get; set; } = "Chờ xử lý";
        public virtual Tour? Tour { get; set; } 
        public string? EmailNguoiDat { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string? TenKhachHang { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string? SoDienThoai { get; set; }

        [Required]
        public int SoNguoi { get; set; } // Đi mấy người

        [Required]
        public DateTime NgayDi { get; set; } // Ngày bắt đầu đi
        
        public DateTime NgayDat { get; set; } = DateTime.Now; // Tự động lấy giờ hiện tại khi bấm nút
    }
}