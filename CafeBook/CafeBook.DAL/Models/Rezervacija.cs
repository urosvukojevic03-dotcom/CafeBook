namespace CafeBook.DAL.Models
{
    public class Rezervacija
    {
        public int Id { get; set; }
        public string ImeGosta { get; set; } = string.Empty;
        public string BrojTelefona { get; set; } = string.Empty;
        public DateTime DatumVreme { get; set; }
        public int BrojOsoba { get; set; }
        public int StolId { get; set; }
        public  Stol? Stol { get; set; }
}
}