using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.Models
{
    public class PedidoViewModel : PadraoViewModel
    {
        public int id_Cliente { get; set; }
        public DateTime data { get; set; }
        public double valor { get; set; }
        public int quantidade { get; set; }
        public int id_Status { get; set; }
        public int prazo { get; set; }
        public int id_Pagamento { get; set; }
        public int id_Carrinho { get; set; }
    }
}
