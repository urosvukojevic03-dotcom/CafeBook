
using CafeBook.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeBook.Buisiness
{
    public class RezervacijaServis
    {
        private readonly CafeBookContext _context;

        public RezervacijaServis(CafeBookContext context)
        {
            _context = context;
        }

        // Dohvati sve stolove
        public async Task<List<Stol>> GetSviStolovi()
        {
            return await _context.Stolovi.ToListAsync();
        }

        // Proveri da li je sto zauzet
        public async Task<bool> JeStoZauzet(int stolId, DateTime datumVreme)
        {
            return await _context.Rezervacije.AnyAsync(r =>
                r.StolId == stolId &&
                r.DatumVreme >= datumVreme.AddHours(-2) &&
                r.DatumVreme <= datumVreme.AddHours(2));
        }

        // Kreiraj rezervaciju
        public async Task KreirajRezervaciju(Rezervacija rezervacija)
        {
            _context.Rezervacije.Add(rezervacija);
            await _context.SaveChangesAsync();
        }

        // Dohvati zauzete stolove
        public async Task<List<int>> GetZauzetiStolovi()
        {
            var sada = DateTime.Now;
            return await _context.Rezervacije
                .Where(r => r.DatumVreme >= sada.AddHours(-2) &&
                            r.DatumVreme <= sada.AddHours(2))
                .Select(r => r.StolId)
                .ToListAsync();
        }
    }
}