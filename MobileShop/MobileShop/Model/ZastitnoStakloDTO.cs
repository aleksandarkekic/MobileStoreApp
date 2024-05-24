using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShop.Model
{

   public class ZastitnoStakloDTO
    {

        public string model { get; set; }
        public short cijena { get; set; }
        public short brojUredjajaNaStanju { get; set; }
        public ZastitnoStakloDTO() { }
    }
}
