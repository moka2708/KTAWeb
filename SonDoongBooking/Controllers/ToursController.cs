using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SonDoongBooking.Data;
using SonDoongBooking.Models;

namespace SonDoongBooking.Controllers
{
    public class ToursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Xem danh sách Tour (Trang Index)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tours.ToListAsync());
        }

        // 2. Trang Chi tiết Tour
        public IActionResult Details(int id)
        {
            var tour = _context.Tours.FirstOrDefault(t => t.Id == id);
            if (tour == null) return NotFound("Tour không tồn tại.");
            return View(tour);
        }

        // 3. Hàm GET: Trang Thêm mới Tour
        [Authorize]
        public IActionResult Create()
        {
            if (User.Identity?.Name != "admin@ktanature.com")
                return Unauthorized("Bạn không có quyền!");
            return View();
        }

        // 4. Hàm POST: Xử lý Thêm mới
        [HttpPost]
        [Authorize]
        public IActionResult Create(Tour tour)
        {
            if (User.Identity?.Name != "admin@ktanature.com")
                return Unauthorized("Bạn không có quyền!");

            if (ModelState.IsValid)
            {
                _context.Tours.Add(tour);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // 5. Hàm GET: Trang Chỉnh sửa (Để cậu sửa số chỗ trống/giá tiền)
        [Authorize]
        public IActionResult Edit(int id)
        {
            if (User.Identity?.Name != "admin@ktanature.com")
                return Unauthorized("Bạn không có quyền!");

            var tour = _context.Tours.Find(id);
            if (tour == null) return NotFound();
            return View(tour);
        }

        // 6. Hàm POST: Xử lý Lưu sau khi sửa
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,TenTour,Gia,SoNgay,HinhAnh,MoTa,SoChoConTrong")] Tour tour)
        {
            if (User.Identity?.Name != "admin@ktanature.com")
                return Unauthorized("Bạn không có quyền!");

            if (id != tour.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(tour);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // 7. Trang Thống kê (Dashboard)
        public IActionResult Dashboard()
        {
            var thongKe = _context.Tours.Select(t => new {
                Ten = t.TenTour,
                SoKhach = _context.Bookings.Where(b => b.TourId == t.Id && b.TrangThai != "Đã hủy").Sum(b => (int?)b.SoNguoi) ?? 0,
                DoanhThuDaChot = _context.Bookings.Where(b => b.TourId == t.Id && b.TrangThai == "Đã duyệt").Sum(b => (decimal?)(b.SoNguoi * t.Gia)) ?? 0,
                DoanhThuDuTinh = _context.Bookings.Where(b => b.TourId == t.Id && b.TrangThai == "Chờ xử lý").Sum(b => (decimal?)(b.SoNguoi * t.Gia)) ?? 0
            }).ToList();

            ViewBag.TongDoanhThu = thongKe.Sum(x => x.DoanhThuDaChot);
            ViewBag.TongKhach = thongKe.Sum(x => x.SoKhach);
            ViewBag.TourCaoCap = thongKe.OrderByDescending(x => x.DoanhThuDaChot).FirstOrDefault()?.Ten ?? "Chưa có";

            ViewBag.Labels = System.Text.Json.JsonSerializer.Serialize(thongKe.Select(x => x.Ten));
            ViewBag.DoanhThuDaChotData = System.Text.Json.JsonSerializer.Serialize(thongKe.Select(x => x.DoanhThuDaChot / 1000000));
            ViewBag.DoanhThuDuTinhData = System.Text.Json.JsonSerializer.Serialize(thongKe.Select(x => x.DoanhThuDuTinh / 1000000));
            ViewBag.KhachHangData = System.Text.Json.JsonSerializer.Serialize(thongKe.Select(x => x.SoKhach));

            return View();
        }
    }
}