using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EletroStar.DAO.Principais;
using Microsoft.AspNetCore.Mvc;

namespace EletroStar.Controllers
{
    public class VendasController : PadraoController<EletroStar.Models.ProdutoViewModel>
    {
        public VendasController()
        {
            DAO = new ProdutoDAO();
            GeraProximoId = true;
        }
    }
}