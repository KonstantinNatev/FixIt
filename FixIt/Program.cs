using FixIt.Data;
using FixIt.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация на базата
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=fixit.db"));

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Seed
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//     if (!db.Users.Any())
//     {
//         db.Users.AddRange(new[]
//         {
//             new User { Username = "admin", Password = "admin123", Role = "Admin" },
//             new User { Username = "user", Password = "user123", Role = "User" }
//         });
//         db.SaveChanges();
//     }
// }


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Requests}/{action=Index}/{id?}");

app.Run();
