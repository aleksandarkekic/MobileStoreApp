using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShop.Model
{
    public class Adresa
    {
       public  string brojUlice { get; set; }
       public  string nazivUlice { get; set; }
       public int posta { get; set; }

        public Adresa() { }
        public Adresa(string brojUlice, string nazivUlice, int posta) {
            this.brojUlice = brojUlice;
            this.nazivUlice = nazivUlice;
            this.posta = posta;
        }
    }
}
