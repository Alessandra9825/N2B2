using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.Models
{
    public class EnderecoViewModel : PadraoViewModel
    {
        public int id_Cliente { get; set; }
        public string rua { get; set; }
        public int numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public int id_UF { get; set; }
        public string cep { get; set; }
        public string pais { get; set; }
    }
}
