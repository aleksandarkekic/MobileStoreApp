using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShop.Model
{
    public class MobilniTelefon2DTO
    {
        public string model { get; set; }
        public short cijena { get; set; }   
        public string dijagonalaEkrana { get; set; }
        public string kapacitetBaterije { get; set; }
        public string operativniSistem { get; set; }

        public int tezina { get; set; }

        public string datumObjavljivanja { get; set; }

        public short brojUredjajaNaStanju { get; set; }

        public string procesor { get; set; }
        public string kamera { get; set; }

        public string boja { get; set; }
        public string internaMemorija { get; set; }
        public string radnaMemorija { get; set; }

        public MobilniTelefon2DTO() { }
    }
}
