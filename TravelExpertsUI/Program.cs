using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TravelExpertsData.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add Session
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o => o.LoginPath = "/Account/Login");
builder.Services.AddSession();

//Add TravelExpertContext
builder.Services.AddDbContext<TravelExpertsContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("TravelExpertsConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStatusCodePages();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //Needed for authentication
app.UseAuthorization();

app.UseSession(); //Needed to use session state object

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
