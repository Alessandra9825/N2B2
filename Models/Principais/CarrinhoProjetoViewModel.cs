using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.Models.Principais
{
    public class CarrinhoProjetoViewModel: PadraoViewModel
    {
        public int id_Projeto { get; set; }

        public string nome { get; set; }

        public int quantidade { get; set; }

        public double valor { get; set; }
    }
}
