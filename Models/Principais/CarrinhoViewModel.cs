using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.Models.Principais
{
    public class CarrinhoViewModel : PadraoViewModel
    {
        public int id_Cliente { get; set; }

        public int quantidade { get; set; }
    }
}
