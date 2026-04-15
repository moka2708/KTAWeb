using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SonDoongBooking.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Kết nối Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 2. Cấu hình Identity (Tớ đã tắt RequireConfirmedAccount để cậu test cho dễ)
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false; // Tắt cái này để đăng ký xong đăng nhập được luôn
    options.Password.RequireDigit = false; // Cho phép đặt mật khẩu đơn giản để test nhanh
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection(); // Tạm tắt nếu cậu chạy localhost cho nhẹ
app.UseStaticFiles();

app.UseRouting();

// BỘ ĐÔI BẢO VỆ PHẢI ĐI LIỀN NHAU VÀ ĐÚNG THỨ TỰ:
app.UseAuthentication(); // Bước 1: Kiểm tra xem ông là ai?
app.UseAuthorization();  // Bước 2: Sau khi biết là ai rồi mới kiểm tra có quyền đặt tour không?

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Cần thiết để chạy các trang Đăng nhập/Đăng ký của Identity

app.Run();