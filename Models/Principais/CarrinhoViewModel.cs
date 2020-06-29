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
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }
        public string ImagemEmBase64 { get; set; }
    }
}
