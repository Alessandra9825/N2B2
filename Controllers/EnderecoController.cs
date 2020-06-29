using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO;
using EletroStar.Models;
using Microsoft.AspNetCore.Mvc;

namespace EletroStar.Controllers
{
    public class EnderecoController : PadraoController<EnderecoViewModel>
    {
        public EnderecoController()
        {
            DAO = new EnderecoDAO();
            GeraProximoId = true;
        }                       
    }
}