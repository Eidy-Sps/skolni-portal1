using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Skolni_portal.Data;
<<<<<<< HEAD

var builder = WebApplication.CreateBuilder(args);
=======

namespace Skolni_portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add database context
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
>>>>>>> origin/Franta

// 1. Připojení databáze
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Nastavení Identity (přihlašování)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    // Tady nastavujeme pravidla pro hesla
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

<<<<<<< HEAD
var app = builder.Build();
=======
            app.UseAuthentication();
            app.UseAuthorization();
>>>>>>> origin/Franta

// ... (tady zůstává původní kód pro Error handling a HSTS) ...

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 3. ZAPNUTÍ AUTENTIZACE! (Musí být přesně v tomto pořadí)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();