using CafeBook.Buisiness;
using CafeBook.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeBook.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminServis _servis;

        public AdminController(AdminServis servis)
        {
            _servis = servis;
        }

        public IActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Prijava(string korisnickoIme, string lozinka)
        {
            var korisnik = await _servis.Prijava(korisnickoIme, lozinka);

            if (korisnik != null)
            {
                HttpContext.Session.SetString("Admin", korisnik.KorisnickoIme);
                return RedirectToAction("Rezervacije");
            }

            ViewBag.Greska = "Pogrešno korisničko ime ili lozinka!";
            return View();
        }

        public async Task<IActionResult> Rezervacije()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Prijava");

            var rezervacije = await _servis.GetSveRezervacije();
            return View(rezervacije);
        }

        public async Task<IActionResult> Obrisi(int id)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Prijava");

            await _servis.ObrisiRezervaciju(id);
            return RedirectToAction("Rezervacije");
        }

        public IActionResult Odjava()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Prijava");
        }
    }
}