using CafeBook.Buisiness;
using CafeBook.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeBook.Controllers
{
    public class RezervacijaController : Controller
    {
        private readonly RezervacijaServis _servis;

        public RezervacijaController(RezervacijaServis servis)
        {
            _servis = servis;
        }

        public async Task<IActionResult> Index()
        {
            var stolovi = await _servis.GetSviStolovi();
            return View(stolovi);
        }

        [HttpPost]
        public async Task<IActionResult> Kreiraj(Rezervacija rezervacija)
        {
            bool zauzet = await _servis.JeStoZauzet(
                rezervacija.StolId, rezervacija.DatumVreme);

            if (zauzet)
            {
                var stolovi = await _servis.GetSviStolovi();
                ViewBag.Greska = "Izabrani sto je već rezervisan u ovom terminu. Molimo odaberite drugi sto ili vreme.";
                return View("Index", stolovi);
            }

            await _servis.KreirajRezervaciju(rezervacija);
            return RedirectToAction("Uspeh");
        }

        public IActionResult Uspeh()
        {
            return View();
        }

        public async Task<IActionResult> Stolovi()
        {
            var stolovi = await _servis.GetSviStolovi();
            var zauzetiStolovi = await _servis.GetZauzetiStolovi();
            ViewBag.ZauzetiStolovi = zauzetiStolovi;
            return View(stolovi);
        }
    }
}