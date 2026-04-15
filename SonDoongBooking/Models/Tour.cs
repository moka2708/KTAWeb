using System.ComponentModel.DataAnnotations;


namespace SonDoongBooking.Models
{
    public class Tour
    {
        [Key] // Đánh dấu đây là Khóa chính (Mã ID tự động tăng)
        public int Id { get; set; }
        
        [Required]
        public string? TenTour { get; set; } // Ví dụ: Khám phá Sơn Đoòng 4N3Đ
        
        public string? MoTa { get; set; } // Giới thiệu qua về tour
        
        public decimal Gia { get; set; } // Giá tiền (ví dụ: 70000000)
        
        public int SoNgay { get; set; } // Số ngày đi (ví dụ: 4)
        public int SoChoConTrong { get; set; } = 20;
        
        public string? HinhAnh { get; set; } // Đường dẫn ảnh minh họa cho tour
    }
}