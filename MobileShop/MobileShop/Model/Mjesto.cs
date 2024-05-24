using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShop.Model
{
   public class Mjesto
    {
        public int posta { get; set; }
        public string naziv { get; set; }

        public Mjesto() { }

        public Mjesto(int posta, string naziv) {
            this.posta = posta;
            this.naziv = naziv;
        }
    }
}
