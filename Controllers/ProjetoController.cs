using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EletroStar.Models.Principais;
using EletroStar.DAO.Principais;

namespace EletroStar.Controllers
{
    public class ProjetoController : PadraoController<ProjetoViewModel>
    {
        public ProjetoController()
        {
            DAO = new ProjetoDAO();
            GeraProximoId = true;
        }

        public IActionResult AtualizaGridIndex(string nome)
        {
            List<ProjetoViewModel> lista;
            if (nome == null)
                lista = ((DAO as ProjetoDAO).ListagemComFiltro(" "));
            else
                lista = ((DAO as ProjetoDAO).ListagemComFiltro(nome));

            return PartialView("pvProjeto", lista);
        }
        protected override void ValidaDados(ProjetoViewModel model, string operacao, string? confsenha, string? confemail)
        {
            base.ValidaDados(model, operacao, confsenha, confemail);
            if (string.IsNullOrEmpty(model.descricao))
                ModelState.AddModelError("descricao", "Preencha a descrição do projeto.");

            if (string.IsNullOrEmpty(model.nome))
                ModelState.AddModelError("nome", "Preencha o nome do projeto.");

            if (string.IsNullOrEmpty(model.link))
                ModelState.AddModelError("link", "Preencha o nome do link.");

        }

        protected override void PreencheDadosParaView(string operacao, ProjetoViewModel model)
        {
            //utiliza os dados do pai
            base.PreencheDadosParaView(operacao, model);           
        }
    }
}