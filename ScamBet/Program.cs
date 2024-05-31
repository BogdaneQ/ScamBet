using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScamBet.Controllers;
using ScamBet.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookmacherDBContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<AccountController>();
builder.Services.AddScoped<MatchController>();
builder.Services.AddScoped<RouletteController>();
builder.Services.AddScoped<TeamController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "Home",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();