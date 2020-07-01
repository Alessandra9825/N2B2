using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EletroStar.Models.Principais;
using EletroStar.DAO.Principais;
using EletroStar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

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

            ProdutoDAO dao = new ProdutoDAO();
            var produtos = dao.Listagem();

            List<SelectListItem> listaProdutos = new List<SelectListItem>();
            listaProdutos.Add(new SelectListItem("Selecione seus Produtos...", "0"));

            foreach (var produto in produtos)
                listaProdutos.Add(new SelectListItem(produto.descricao, produto.id.ToString()));

            ViewBag.Produtos = listaProdutos;
        }
        
        public IActionResult SalvarProjeto(ProjetoViewModel model, List<int> IdProdutos, string operacao, string? confsenha, string? confemail)
        {
            try
            {
                ModelState.Clear();

                ViewBag.Logado = HelperController.VerificaUserLogado(HttpContext.Session);
                ViewBag.Admin = HelperController.VerificaUserAdmin(HttpContext.Session);
                ViewBag.IdCliente = Convert.ToInt32(HelperController.IdCliente(HttpContext.Session));

                ValidaDados(model, operacao, confsenha, confemail);

                if (IdProdutos.Count == 0)
                    throw new Exception("Selecione os produtos do projeto!");
                                
                if (ModelState.IsValid == false)
                {
                    ViewBag.operacao = operacao;
                    PreencheDadosParaView(operacao, model);
                    return View("Form", model);
                }
                else
                {
                    ProjetoDAO dao = new ProjetoDAO();

                    if (operacao == "I")
                        dao.Insert(model, IdProdutos);
                    else
                        dao.Update(model, IdProdutos);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                ViewBag.operacao = operacao;
                PreencheDadosParaView(operacao, model);
                return View("Form", model);
            }
        }
        
    }
}