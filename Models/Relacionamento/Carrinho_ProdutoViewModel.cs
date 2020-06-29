using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.Models.Relacionamento
{
    public class Carrinho_ProdutoViewModel : PadraoViewModel
    {
        public int id_Carrinho { get; set; }

        public int id_Produto { get; set; }        

        public int quantidade { get; set; }
    }
}
