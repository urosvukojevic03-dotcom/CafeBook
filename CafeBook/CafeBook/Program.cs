using CafeBook.Buisiness;
using CafeBook.DAL.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CafeBookContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

// Registruj BLL servise
builder.Services.AddScoped<RezervacijaServis>();
builder.Services.AddScoped<AdminServis>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CafeBookContext>();

    // ✔️ migracije
    context.Database.Migrate();

    // ✔️ admin
    if (!context.Korisnici.Any())
    {
        context.Korisnici.Add(new Korisnik
        {
            KorisnickoIme = "admin",
            Lozinka = "admin123",
            JeAdmin = true
        });

        context.SaveChanges();
    }

    // ✔️ stolovi (ODVOJENO!)
    if (!context.Stolovi.Any())
    {
        context.Stolovi.AddRange(
            new Stol { BrojStola = 1, Kapacitet = 2, Lokacija = "Unutra" },
            new Stol { BrojStola = 2, Kapacitet = 4, Lokacija = "Unutra" },
            new Stol { BrojStola = 3, Kapacitet = 4, Lokacija = "Terasa" },
            new Stol { BrojStola = 4, Kapacitet = 6, Lokacija = "Terasa" },
            new Stol { BrojStola = 5, Kapacitet = 2, Lokacija = "Bar" }
        );

        context.SaveChanges();
    }
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();