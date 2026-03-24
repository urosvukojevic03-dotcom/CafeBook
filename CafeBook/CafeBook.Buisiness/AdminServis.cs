
using CafeBook.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeBook.Buisiness
{
    public class AdminServis
    {
        private readonly CafeBookContext _context;

        public AdminServis(CafeBookContext context)
        {
            _context = context;
        }

        // Prijava admina
        public async Task<Korisnik?> Prijava(string korisnickoIme, string lozinka)
        {
            return await _context.Korisnici
                .FirstOrDefaultAsync(k =>
                    k.KorisnickoIme == korisnickoIme &&
                    k.Lozinka == lozinka &&
                    k.JeAdmin == true);
        }

        // Dohvati sve rezervacije
        public async Task<List<Rezervacija>> GetSveRezervacije()
        {
            return await _context.Rezervacije
                .Include(r => r.Stol)
                .ToListAsync();
        }

        // Obrisi rezervaciju
        public async Task ObrisiRezervaciju(int id)
        {
            var rezervacija = await _context.Rezervacije.FindAsync(id);
            if (rezervacija != null)
            {
                _context.Rezervacije.Remove(rezervacija);
                await _context.SaveChangesAsync();
            }
        }
    }
}