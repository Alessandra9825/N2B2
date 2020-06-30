using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.Models.Principais
{
    public class CarrinhoProdutoViewModel : PadraoViewModel
    {
        public int id_Produto { get; set; }

        public string descricao { get; set; }

        public string nome { get; set; }

        public double valor { get; set; }

        public int quantidade { get; set; }

        public IFormFile Imagem { get; set; }

        public byte[] ImagemEmByte { get; set; }

        /// <summary>
        /// Imagem usada para ser enviada ao form no formato para ser exibida
        /// </summary>
        public string ImagemEmBase64
        {
            get
            {
                if (ImagemEmByte != null)
                    return Convert.ToBase64String(ImagemEmByte);
                else
                    return string.Empty;
            }
        }
    }
}
