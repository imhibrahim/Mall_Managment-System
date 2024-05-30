using Mall_Managment_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var provide = builder.Services.BuildServiceProvider();
var config = provide.GetRequiredService<IConfiguration>();

//Shops Use
builder.Services.AddDbContext<ShopDbContext>(options=>options.
UseSqlServer(config.GetConnectionString("DefaultConnection")));

//Movies Use
builder.Services.AddDbContext<MovieDbContext>(options => options.
UseSqlServer(config.GetConnectionString("DefaultConnection")));


//Items Use
builder.Services.AddDbContext<ItemDbContext>(options => options.
UseSqlServer(config.GetConnectionString("DefaultConnection")));

//food court Use
builder.Services.AddDbContext<FoodCourtDbContext>(options => options.
UseSqlServer(config.GetConnectionString("DefaultConnection")));

//Gallery Use
builder.Services.AddDbContext<GallaryDbContext>(options => options.
UseSqlServer(config.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();
