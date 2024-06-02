using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScamBet.Controllers;
using ScamBet.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Access/Login";
    });

builder.Services.AddDbContext<BookmacherDBContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<AccountController>();
builder.Services.AddScoped<MatchController>();
builder.Services.AddScoped<RouletteController>();
builder.Services.AddScoped<TeamController>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BookmacherDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Access/Login";
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area:exists}/{controller=Access}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "account",
    pattern: "{area:exists}/{controller=Account}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "match",
    pattern: "{area:exists}/{controller=Match}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "rulette",
    pattern: "{area:exists}/{controller=Rulette}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "team",
    pattern: "{area:exists}/{controller=Team}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Home",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "Home",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();