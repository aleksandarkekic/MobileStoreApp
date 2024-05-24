namespace MobileShop.Model
{
   public class Korisnik
    {
        public int id { set; get; }
        public int idAdrese { set; get; }
        public string ime { set; get; }
        public string prezime { set; get; }
        public string sifra { set; get; }
        public string korisnickoIme { set; get; }
        public string uloga { set; get; }


        public Korisnik() { }

        public Korisnik(int id, string ime, string prezime, string sifra, string korisnickoIme, int idAdrese,string uloga)
        {
            this.id = id;
            this.ime = ime;
            this.prezime = prezime;
            this.sifra = sifra;
            this.korisnickoIme = korisnickoIme;
            this.idAdrese = idAdrese;
            this.uloga = uloga;

        }

    }
}
