using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.Models
{
    public class ProdutoViewModel : PadraoViewModel
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public double valor { get; set; }
        public int id_Fabricante { get; set; }
        public int id_Categoria { get; set; }
        public string Fabricante { get; set; }
        public string Categoria { get; set; }
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
