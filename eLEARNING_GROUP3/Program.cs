using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Configurations;
using OnlineLearning.Data;
using OnlineLearning.Hubs;
using OnlineLearning.Repositories;
using OnlineLearning.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext với chuỗi kết nối từ appsettings.json
builder.Services.AddDbContext<OnlLearnDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình appsettings
builder.Configuration.ConfigureAppSettings(builder);

//cau hinh view areas
builder.Services.ConfigureRazorViewEngine();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add signalR
builder.Services.AddSignalR();

//Add HttpClient
builder.Services.AddHttpClient();

// Cấu hình Database
builder.Services.AddDbContextConfiguration(builder.Configuration);

// Cấu hình DI
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);

// Cấu hình Login Gooogle
builder.Services.ConfigureAuthentication(builder.Configuration);



// Cấu hình Session 
builder.Services.ConfigureSession();


//// Cấu hình Authorization
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy =>
//    {
//        policy.RequireRole("Admin"); // Yêu cầu role "Admin"
//    });
//});


//build
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Cau hinh signalR
app.MapHub<ChatHub>("/chathub");
app.MapHub<CourseNotificationHub>("/courseNotificationHub");
app.MapHub<UserChatHub>("/userChatHub");

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//Kiem tra area truoc
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
