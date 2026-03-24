namespace CafeBook.DAL.Models
{
    public class Stol
    {
        public int Id { get; set; }
        public int BrojStola { get; set; }
        public int Kapacitet { get; set; }
        public string Lokacija { get; set; } = string.Empty;
        public ICollection<Rezervacija> Rezervacije { get; set; } = new List<Rezervacija>();
    }
}