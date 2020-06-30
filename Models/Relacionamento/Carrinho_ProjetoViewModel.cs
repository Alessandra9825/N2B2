using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.Models.Relacionamento
{
    public class Carrinho_ProjetoViewModel: PadraoViewModel
    {
        public int id_Carrinho { get; set; }

        public int id_Projeto { get; set; }

        public int quantidade { get; set; }
    }
}
