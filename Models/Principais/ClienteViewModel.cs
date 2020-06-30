using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletroStar.Models
{
    public class ClienteViewModel : PadraoViewModel
    {
        public int consulta { get; set; }
        public string cpf { get; set; }

        public string nome { get; set; }

        public int id_EstadoCivil { get; set; }

        public string email { get; set; }

        public DateTime nascimento { get; set; }

        public char genero { get; set; }

        public string tel_residencial { get; set; }

        public string tel_celular { get; set; }

        public bool admin { get; set; }

        public string senha { get; set; }

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
        public static DateTime Today { get; }


    }
}

