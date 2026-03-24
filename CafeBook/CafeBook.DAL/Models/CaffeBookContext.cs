using Microsoft.EntityFrameworkCore;
using CafeBook;
namespace CafeBook.DAL.Models
{
    public class CafeBookContext : DbContext
    {
        public CafeBookContext(DbContextOptions<CafeBookContext> options)
            : base(options) { }

        public DbSet<Stol> Stolovi { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
    }
}