using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperTix.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SuperTixContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SuperTixContext") ?? throw new InvalidOperationException("Connection string 'SuperTixContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();


// Add user cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true; // Reset the expiration time if the user is active
        options.LoginPath = "/Account/Login"; // re-direct to login page
        options.LogoutPath = "/Account/Logout"; // re-direct to logout page
        options.AccessDeniedPath = "/Account/AccessDenied"; // re-direct to access denied page
    });


if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Games}/{action=Index}/{id?}");

app.Run();
