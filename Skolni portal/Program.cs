using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Skolni_portal.Data;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

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