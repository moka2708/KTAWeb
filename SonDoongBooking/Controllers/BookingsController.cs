using Microsoft.AspNetCore.Mvc;
using SonDoongBooking.Data;
using SonDoongBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace SonDoongBooking.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // 1. DÀNH CHO GIÁM ĐỐC: Xem TOÀN BỘ danh sách đơn
        // =========================================================
        [Authorize]
        public IActionResult Index()
        {
            if (User.Identity?.Name != "admin@ktanature.com")
            {
                return Unauthorized("Chỉ Giám đốc (Admin) mới được xem danh sách này!");
            }

            var danhSachDatCho = _context.Bookings
                                         .Include(b => b.Tour) 
                                         .OrderByDescending(b => b.NgayDat)
                                         .ToList();

            return View(danhSachDatCho);
        }

        // =========================================================
        // 2. DÀNH CHO KHÁCH HÀNG: XEM LỊCH SỬ ĐẶT CỦA RIÊNG MÌNH
        // =========================================================
        [Authorize]
        public IActionResult MyBookings()
        {
            var emailHienTai = User.Identity?.Name;

            // QUAN TRỌNG: Lọc theo cột EmailNguoiDat mới chuẩn
            var lichSuCuaToi = _context.Bookings
                                       .Include(b => b.Tour)
                                       .Where(b => b.EmailNguoiDat == emailHienTai)
                                       .OrderByDescending(b => b.NgayDat)
                                       .ToList();

            return View(lichSuCuaToi);
        }

        // =========================================================
        // 3. DÀNH CHO KHÁCH HÀNG: Mở form đặt Tour
        // =========================================================
        [Authorize]
        public IActionResult Create(int tourId)
        {
            var tour = _context.Tours.FirstOrDefault(t => t.Id == tourId);
            if (tour == null) return NotFound("Không tìm thấy Tour này!");

            ViewBag.TenTour = tour.TenTour;
            ViewBag.Gia = tour.Gia;

            var booking = new Booking { TourId = tourId };
            
            // Đã truyền số chỗ còn trống cho JavaScript bắt lỗi
            ViewBag.SoChoConTrong = tour.SoChoConTrong; 
            return View(booking);
        }

        // =========================================================
        // 4. DÀNH CHO KHÁCH HÀNG: Xử lý lưu đơn (ĐÃ GẮN BẢO VỆ)
        // =========================================================
        [HttpPost]
        [Authorize]
        public IActionResult Create(Booking booking)
        {
            ModelState.Remove("Tour");
            
            if (ModelState.IsValid)
            {
                // TÌM LẠI TOUR ĐỂ KIỂM TRA SỐ LƯỢNG CHỖ THỰC TẾ
                var tour = _context.Tours.FirstOrDefault(t => t.Id == booking.TourId);

                if (tour != null)
                {
                    // LỚP BẢO VỆ LÕI THÉP: Chặn đứng nếu đặt lố số chỗ
                    if (booking.SoNguoi > tour.SoChoConTrong)
                    {
                        ModelState.AddModelError("", $"Rất tiếc! Tour này hiện chỉ còn {tour.SoChoConTrong} chỗ.");
                        
                        // Truyền lại dữ liệu để form không bị lỗi mờ chữ
                        ViewBag.TenTour = tour.TenTour;
                        ViewBag.Gia = tour.Gia;
                        ViewBag.SoChoConTrong = tour.SoChoConTrong;
                        return View(booking);
                    }

                    // KHÓA CỔNG: Trừ ngay số ghế để người sau không đặt được nữa
                    tour.SoChoConTrong -= booking.SoNguoi;
                    _context.Tours.Update(tour);

                    // LƯU EMAIL TÀI KHOẢN VÀO CỘT RIÊNG
                    booking.EmailNguoiDat = User.Identity?.Name;
                    
                    // KHÔNG đè lên TenKhachHang nữa, để lấy tên thật từ Form
                    booking.NgayDat = DateTime.Now;
                    booking.TrangThai = "Chờ xử lý";

                    _context.Bookings.Add(booking);
                    _context.SaveChanges();
                    
                    TempData["ThongBao"] = "Đặt tour thành công! Theo dõi ở mục Chuyến đi của tôi nhé.";
                    return RedirectToAction(nameof(Payment));
                }
            }

            // Nạp lại dữ liệu nếu Form bị lỗi vặt
            var fallbackTour = _context.Tours.FirstOrDefault(t => t.Id == booking.TourId);
            if (fallbackTour != null) { 
                ViewBag.TenTour = fallbackTour.TenTour; 
                ViewBag.Gia = fallbackTour.Gia; 
                ViewBag.SoChoConTrong = fallbackTour.SoChoConTrong;
            }
            return View(booking);
        }

        public IActionResult Payment()
        {
            ViewBag.MaGiaoDich = "KTA" + new Random().Next(100000, 999999).ToString();
            return View();
        }

        // =========================================================
        // 5. CHI TIẾT ĐƠN HÀNG (Có bảo mật)
        // =========================================================
        [Authorize]
        public IActionResult Details(int id)
        {
            var donHang = _context.Bookings.Include(b => b.Tour).FirstOrDefault(m => m.Id == id);

            if (donHang == null) return NotFound();

            var currentUserName = User.Identity?.Name ?? "";

            // Kiểm tra: Admin hoặc chủ nhân của đơn (dựa trên EmailNguoiDat) mới được xem
            if (currentUserName != "admin@ktanature.com" && currentUserName != donHang.EmailNguoiDat)
            {
                return Unauthorized("Bạn không có quyền xem đơn hàng của người khác!");
            }

            return View(donHang);
        }

        // =========================================================
        // 6. DÀNH CHO ADMIN: Duyệt đơn và chỉnh sửa
        // =========================================================
        [Authorize]
        public IActionResult Edit(int id)
        {
            if (User.Identity?.Name != "admin@ktanature.com") return Unauthorized();

            var donHang = _context.Bookings.Find(id);
            if (donHang == null) return NotFound();
            return View(donHang);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, Booking booking)
        {
            if (User.Identity?.Name != "admin@ktanature.com") return Unauthorized();

            if (id != booking.Id) return NotFound();

            var donHangCu = _context.Bookings.AsNoTracking().FirstOrDefault(b => b.Id == id);
            if (donHangCu == null) return NotFound();

            var tour = _context.Tours.Find(donHangCu.TourId);

            if (tour != null)
            {
                // BỎ LOGIC TRỪ CHỖ khi Duyệt (Vì hệ thống đã tự trừ ngay lúc khách bấm XÁC NHẬN ĐẶT TOUR)
                // CHỈ XỬ LÝ KHI HỦY: Nếu Admin hủy đơn thì cộng trả lại ghế cho Tour
                if (booking.TrangThai == "Đã hủy" && donHangCu.TrangThai != "Đã hủy")
                {
                    tour.SoChoConTrong += donHangCu.SoNguoi;
                    _context.Tours.Update(tour);
                }
            }

            _context.Bookings.Update(booking);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}