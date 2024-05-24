using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShop.Model
{
    public class KorisnikDTO
    {
       
        public int id { set; get; }
        public string ime { set; get; }
        public string prezime { set; get; }
        public string korisnickoIme { set; get; }
        public string uloga { set; get; }


        public KorisnikDTO() { }
       
      

    }
}
