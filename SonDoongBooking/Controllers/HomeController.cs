using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SonDoongBooking.Data;
using SonDoongBooking.Models;

namespace SonDoongBooking.Controllers
{
    public class HomeController : Controller
    {
        // Khai báo công cụ kết nối Database
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Mở trang chủ
        public IActionResult Index()
        {
            // Lấy toàn bộ Tour trong Database
            var danhSachTour = _context.Tours.ToList();
            
            // Ném sang cho file Giao diện (Index.cshtml)
            return View(danhSachTour); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}